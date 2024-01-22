# ConsoleApp1C#
Проект по написанию консольной утилиты на C# для вычисления индекса массы тела (англ. body mass index (BMI), ИМТ).
Проект использует функцию ввода данных пользователем с консоли для анализа этих данных и вывода о BMI пользователя.
Написан на C#, в системе Microsoft Visual Studio, выложен на Github с использованием Visual Studio.
Код программы находится в папке ConsoleApp1C#, в файле Program.cs.

Для запуска проекта необходимо запустить файл Program.cs. При запуске программа начнет запрашивать требуемые для вычислений параметры. Десятичные дроби вводить в зависимости от компилятора, через точку или запятую.

Главная часть кода здесь:

            double height = GetUnit("height");
            double weight = GetUnit("weight");
            double bmi = CalculateBMI(weight, height);
            string bmiCategory = GetBMICategory(bmi);

            Console.WriteLine($"IBM = {bmi:F3} - {bmiCategory}");
            Console.ReadLine();
            
GetUnit это функция, выполняющая три действия: запрос единиц измерения величины у пользвателя, запрос самого значения величины и нормализация значения для подстановки в формулу (если в формуле используются метры, то наше значение нужно перевести в метры, аналогично с массой).
CalculateBMI это функция, подставляющая нормализованные значения веса и высоты в формулу.
BMICategory это функция, определяюзая категорию "Below Average", "Average" и "Above Average" BMI на основе заданных параметров: 

        public static double belowAverageBMI = 18.5;
        public static double aboveAverageBMI = 25;
        
Программа концентрируется на гибкости используемы данных, и ее можно легко достроить для запроса и анализа новых параметров на основе других единиц измерения. На основе данной систмы можно без труда добавить новую запрашиваемую величину, и на основе ее использования написать функции для анализа и подсчета других данных. К примеру, для добавления запроса о новой гипотетической величине Money, выражающейся в единицах "Dollar", "Ruble", "Yen", мы допишем словарь Parameters, вставив в него код:

            {
                "Money", new Dictionary<string, Func<double, double>>
                {
                    {"Dollars", value => value},
                    {"Rubles", value => value * 0.014},
                    {"Yen", value => value * 0.01},
                }
            }
            
И затем, чтобы запросить у пользователя днную величину, ее единицы измерения и нормализовать ее, нам достаточно использовать команду: 

            double money = GetUnit("Money");
            
и полученную величину можно использовать в дальнейших вычислениях.
