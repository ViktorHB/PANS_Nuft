using Microsoft.Expression.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using PANS.Entities;
using PANS.Services;
using PANS.UI;
using Server;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.MessageBox;

namespace PANS
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly string _bFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "___NUFT", "PANS.db");
        readonly ITechTableSqliteDataAccess _dataAccessTech;
        readonly IQuizTableSqliteDataAccess _dataAccessQuiz;
        private readonly MainWindow _mainWindow;
        IDbService _dbService;
        private static string AboutProject = @"
1. Створити інтерфейс електронного засобу навчання (ЕЗН) із теоретичним матеріалом із обраного предмету, з певної теми (матеріал для наповнення — довільний). 
Реалізувати можна будь-якою мовою програмування та в будь-якому вигляді: або як десктопну програму з вікнами (формами), або як веб-сайт зі сторінками,
або як мобільний додаток. При виборі технологій розроблення зверніть увагу, що в наступних ЛР цей ЕЗН треба буде доповнити функціями тренування,
тестування, зв’язку з БД, алгоритмами керування процесом навчання та контролю знань. Тобто це буде значно більше, ніж набір сторінок.

2. Має бути 5–10 сторінок (вікон) із текстом (залежно від предмету можливо також формулами тощо).

3. На кожній сторінці має бути мінімум по 1 ілюстрації.

4. Кожна сторінка повинна містити кнопки навігації між ними, аби студент / учень гортав їх, як книгу.

5. При реалізації графічного інтерфейсу дотримуватись основних вимог до дизайну, описаних у лекції 2 (кольори, шрифти, зрозумілість).
6. Розмістити інформацію про автора курсу в довільному місці ЕЗН та показати це у звіті.

7. Завдання на максимальний бал: передбачити налаштовуваність (адаптивність) графічного інтерфейсу користувача, зокрема як
мінімум — можливість зміни користувачем розміру шрифту при перегляді навчальної інформації.

";
        public string CSharpText { get; set; } =
            @"C# (pronounced C sharp) is a general-purpose high-level programming language supporting multiple paradigms.
 C# encompasses static typing, strong typing, lexically scoped, imperative, declarative, functional, generic, object-oriented 
(class-based), and component-oriented programming disciplines.
The C# programming language was designed by Anders Hejlsberg from Microsoft in 2000 and was later approved as an international 
standard by Ecma (ECMA-334) in 2002 and ISO/IEC (ISO/IEC 23270) in 2003. Microsoft introduced C# along with .NET Framework and Visual Studio,
both of which were closed-source. At the time, Microsoft had no open-source products. Four years later, in 2004, a free and open-source project 
called Mono began, providing a cross-platform compiler and runtime environment for the C# programming language. A decade later, Microsoft released 
Visual Studio Code (code editor), Roslyn (compiler), and the unified .NET platform (software framework), all of which support C# and are free, open-source,
and cross-platform. Mono also joined Microsoft but was not merged into .NET.
As of November 2022, the most recent stable version of the language is C# 11.0, which was released in 2022 in .NET 7.0.";

        public string WPFText { get; set; } = @"Windows Presentation Foundation (WPF) is a free and open-source graphical subsystem (similar to WinForms)
originally developed by Microsoft for rendering user interfaces in Windows-based applications. WPF, previously known as 
""Avalon"", was initially released as part of .NET Framework 3.0 in 2006. WPF uses DirectX and attempts to 
provide a consistent programming model for building applications. It separates the user interface from business logic,
and resembles similar XML-oriented object models, such as those implemented in XUL and SVG.";

        public string WCFText { get; set; } = @"The Windows Communication Foundation (WCF), previously known as Indigo, is a free and open-source runtime and a set of 
APIs in the .NET Framework for building connected, service-oriented applications.
.NET Core 1.0, released 2016, did not support WCF server side code. WCF support was added to the platform with support for .NET Core 3.1, .NET 5, and .NET 6 in 2022.";

        public string UWPText { get; set; } = @"Universal Windows Platform (UWP) is a computing platform created by Microsoft and first introduced in Windows 10. The purpose of this platform is to help develop universal apps that run on Windows 10,
Windows 10 Mobile (discontinued), Windows 11, Xbox One, Xbox Series X/S, and HoloLens without the need to be rewritten for each. 
It supports Windows app development using C++, C#, VB.NET, and XAML. The API is implemented in C++, and supported in C++, VB.NET, C#, F# and JavaScript. Designed as an extension to the Windows Runtime (WinRT) platform first
introduced in Windows Server 2012 and Windows 8, UWP allows developers to create apps that will potentially run on multiple types of devices.
UWP does not target non-Microsoft systems. Microsoft's solution for other platforms is .NET MAUI (previously ""Xamarin.Forms""), an open-source API 
created by Xamarin, a Microsoft subsidiary since 2016. Community solutions also exist for non-targeted platforms, such as the Uno Platform";

        public string JavaText { get; set; } = @"Java is a high-level, class-based, object-oriented programming language that is designed to have as few implementation 
dependencies as possible. It is a general-purpose programming language intended to let programmers write once, run anywhere (WORA), meaning that compiled Java code 
can run on all platforms that support Java without the need to recompile.[18] Java applications are typically compiled to bytecode that can run on any Java virtual machine 
(JVM) regardless of the underlying computer architecture. The syntax of Java is similar to C and C++, but has fewer low-level facilities than either of them. The Java 
runtime provides dynamic capabilities (such as reflection and runtime code modification) that are typically not available in traditional compiled languages. As of 2019,
Java was one of the most popular programming languages in use according to GitHub,[citation not found][19][20] particularly for client–server web applications, with 
a reported 9 million developers.
Java was originally developed by James Gosling at Sun Microsystems. It was released in May 1995 as a core component of Sun Microsystems' Java platform. The original 
and reference implementation Java compilers, virtual machines, and class libraries were originally released by Sun under proprietary licenses. As of May 2007, in
compliance with the specifications of the Java Community Process, Sun had relicensed most of its Java technologies under the GPL-2.0-only license. Oracle offers its 
own HotSpot Java Virtual Machine, however the official reference implementation is the OpenJDK JVM which is free open-source software and used by most developers and is 
the default JVM for almost all Linux distributions.
As of September 2022, Java 19 is the latest version, while Java 17, 11 and 8 are the current long-term support (LTS) versions.";

        public string JavaScriptText { get; set; } = @"JavaScript (/ˈdʒɑːvəskrɪpt/), often abbreviated as JS, is a programming language that is one of the core 
technologies of the World Wide Web, alongside HTML and CSS. As of 2022, 98% of websites use JavaScript on the client side for webpage behavior, often incorporating 
third-party libraries. All major web browsers have a dedicated JavaScript engine to execute the code on users' devices.
JavaScript is a high-level, often just-in-time compiled language that conforms to the ECMAScript standard. It has dynamic typing, prototype-based object-orientation,
and first-class functions. It is multi-paradigm, supporting event-driven, functional, and imperative programming styles. It has application programming interfaces (APIs)
for working with text, dates, regular expressions, standard data structures, and the Document Object Model (DOM).
The ECMAScript standard does not include any input/output (I/O), such as networking, storage, or graphics facilities. In practice, the web browser or other runtime system 
provides JavaScript APIs for I/O.
JavaScript engines were originally used only in web browsers, but are now core components of some servers and a variety of applications. The most popular runtime 
system for this usage is Node.js.
Although Java and JavaScript are similar in name, syntax, and respective standard libraries, the two languages are distinct and differ greatly in design.";

        public string UWPImageSource { get; set; } = "../../Assets/UWP.png";
        public string JavaImageSource { get; set; } = "../../Assets/Java.png";
        public string JavaScriptImageSource { get; set; } = "../../Assets/JavaScript.png";
        public string CSharpImageSource { get; set; } = "../../Assets/C-Sharp.png";
        public string WPFImageSource { get; set; } = "../../Assets/WPF.png";
        public string WCFImageSource { get; set; } = "./../Assets/WCF.png";

        public ICommand ExitCommand { get; set; }
        public ICommand AboutProjectCommand { get; set; }
        public ICommand AboutStudentCommand { get; set; }
        public ICommand AboutAuthorCommand { get; set; }
        public ICommand FontButtonCommand { get; set; }
        public ICommand ColorButtonCommand { get; set; }
        public ICommand Lab2ButtonCommand { get; set; }
        public ICommand Lab3ButtonCommand { get; set; }
        public ICommand Lab4ButtonCommand { get; set; }
        
        public MainWindowViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _dataAccessTech = new TechTableSqliteDataAccess(_bFilePath);
           _dataAccessQuiz = new QuizTableSqliteDataAccess(_bFilePath);
            ExitCommand = new ActionCommand(ExitCommandExecute);
            AboutProjectCommand = new ActionCommand(AboutProjectCommandExecute);
            AboutStudentCommand = new ActionCommand(AboutStudentCommandExecute);
            AboutAuthorCommand = new ActionCommand(AboutAuthorCommandExecute);
            ColorButtonCommand = new ActionCommand(ColorButtonCommandExecute);
            FontButtonCommand = new ActionCommand(FontButtonCommandExecute);
            Lab2ButtonCommand = new ActionCommand(Lab2ButtonCommandExecute);
            Lab3ButtonCommand = new ActionCommand(Lab3ButtonCommandExecute);

            _dbService = new DbService(_bFilePath)
            {
                QuizDataAccess = _dataAccessQuiz,
                QuizDataRepository = new QuizDataRepository(),
                TechDataRepository = new TechDataRepository(),
                TechDataAccess = _dataAccessTech
            };
        }

    

        private void Lab3ButtonCommandExecute()
        {
            new QuizWindow(new Lab3Window(new Lab3WindowViewModel(_dbService, _dataAccessQuiz, new KnowledgeElementTableSqliteDataAccess(_bFilePath)))).ShowDialog();
        }

        private void Lab2ButtonCommandExecute()
        {
            new Lab2Window(new Lab2WindowViewModel()
            {
                InteractivityControlViewModel = new InteractivityControlViewModel(new SHA1Encryptor()),
                DataBaseControlViewModelViewModel = new DataBaseControlViewModel(new DbService(_bFilePath)
                {
                    TechDataRepository = new TechDataRepository(),
                    TechDataAccess = _dataAccessTech
                }, _dataAccessTech),
                FunctionGraphControlViewModel = new FunctionGraphControlViewModel()
                
            }).ShowDialog();
        }

        private void ColorButtonCommandExecute(object obj)
        {
            var colorDialog = new ColorDialog();
            var result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SetColorStyle(colorDialog, obj as Button);
            }
        }

        private void FontButtonCommandExecute(object obj)
        {
            var fontDialog = new FontDialog();
            var result = fontDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SetFontStyle(fontDialog);
            }
        }

        private void SetColorStyle(ColorDialog dialog, Button button)
        {
            if (dialog == null)
            {
                return;
            }

            if (button != null)
            {
                button.Background = new SolidColorBrush(Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B));
            }

            foreach (var textBlock in FindVisualChilds<TextBlock>(_mainWindow.TabControl))
            {
                textBlock.Foreground =
                    new SolidColorBrush(Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B));
            }
        }

        private void SetFontStyle(FontDialog fontDialog)
        {
            if (fontDialog == null)
            {
                return;
            }

            foreach (var textBlock in FindVisualChilds<TextBlock>(_mainWindow.TabControl))
            {
                textBlock.FontFamily = new FontFamily(fontDialog.Font.Name);
                textBlock.FontSize = fontDialog.Font.Size;
                textBlock.FontWeight = fontDialog.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                textBlock.FontStyle = fontDialog.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
                var tdc = new TextDecorationCollection();
                if (fontDialog.Font.Underline) tdc.Add(TextDecorations.Underline);
                if (fontDialog.Font.Strikeout) tdc.Add(TextDecorations.Strikethrough);
                textBlock.TextDecorations = tdc;
            }
        }

        private void AboutAuthorCommandExecute()
        {
            new AboutCourseAuthorWindow().ShowDialog();
        }

        private void AboutStudentCommandExecute(object obj)
        {
            MessageBox.Show("Viktor \n Group", "About student", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AboutProjectCommandExecute(object obj)
        {
            MessageBox.Show(AboutProject, "About project", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExitCommandExecute(object obj)
        {
            Environment.Exit(1);
        }
        public static IEnumerable<T> FindVisualChilds<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindVisualChilds<T>(ithChild)) yield return childOfChild;
            }
        }
    }
}