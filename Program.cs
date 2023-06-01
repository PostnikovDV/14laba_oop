using _10_laba;
using _13_laba_oop;
using laba_12;
using System;
using System.Diagnostics;
using System.Linq;

namespace _14_laba_oop
{
    static class Program
    {
       
        public static Dictionary<int,Stack<Person>> collection = new Dictionary<int,Stack<Person>>();
        static void Main(string[] args)
        {
            FillStack(5);
            PrintCollection();

            Request1Linq();
            Request1Ext();

            Request2Linq();
            Request2Ext();

            Request3Linq();
            Request3Ext();

            Request4Linq();
            Request4Ext();

            Request5Linq();
            Request5Ext();

            SecondTask();
        }

        public static void AddStack(int count,int f)
        {
            var stck = new Stack<Person>();
            for (int i = 0; i < count; ++i)
            {
                Person toAdd;
                switch ((AddMenu)new Random().Next(1, 5))
                {
                    case AddMenu.Person:
                        {
                            toAdd = new Person().RandomInit();
                            stck.Push(toAdd);
                            break;
                        }
                    case AddMenu.Student:
                        {
                            toAdd = new Student().RandomInit();
                            stck.Push(toAdd);
                            break;
                        }
                    case AddMenu.ParttimeStudent:
                        {
                            toAdd = new ParttimeStudent().RandomInit();
                            stck.Push(toAdd);
                            break;
                        }
                    case AddMenu.SchoolBoy:
                        {
                            toAdd = new Schoolboy().RandomInit();
                            stck.Push(toAdd);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            collection.Add(f,stck);
        }

        public static void FillStack(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                AddStack(count,i);
            }
        }

        public static void PrintCollection()
        {
        
           
           
            foreach(KeyValuePair<int,Stack<Person>> keyValue in collection)
            {
                
                Console.WriteLine($"Key: {keyValue.Key}\nValue: ");
                
                foreach (Person person in keyValue.Value)
                {
                    Console.WriteLine("\t" + person.ToString());
                }
                Console.WriteLine("\t");
                DrawLine();
            }
         
        }
        public static void DrawLine()
        {
            Console.WriteLine("----------------------------------------------------------------------------------------");
        }


        public static void Request1Linq()
        {
            Console.WriteLine("Все люди старше 25 лет\t\tLinq");
            var ans = from stck in collection.Values from person in stck where person.Age > 25 select person;
            foreach(Person person in ans)
            {
                Console.WriteLine(person.ToString());
            }
            DrawLine();
        }
        public static void Request1Ext()
        {
            Console.WriteLine("Все люди старше 25 лет\t\tExtenssion");
            var ans = collection.Values.SelectMany(stck => stck).Select(person => person).Where(person => person.Age > 25);
            foreach (var person in ans)
            {
                Console.WriteLine(person.ToString());
            }
            DrawLine();
        }

        public static void Request2Linq()
        {
            Console.WriteLine("Получение количества студентов \t\tLinq");
            var ans = (from stck in collection.Values from person in stck where person is Student select person).Count();
            Console.WriteLine(ans);
            DrawLine();
        }
        public static void Request2Ext()
        {
            Console.WriteLine("Получение количества студентов\t\tExtenssion");
            var ans = collection.Values.SelectMany(stck => stck).Select(person => person).Where(person => person is Student).Count();
            Console.WriteLine(ans);
            DrawLine();
        }

        public static void Request3Linq()
        {
            Console.WriteLine("Пересечение людей старше 20 и младше 40 \t\tLinq");
            var ans = (from stck in collection.Values from person in stck where person.Age > 20 select person).Intersect
                (from stck in collection.Values from person in stck where person.Age < 40 select person);
            foreach (var item in ans)
            {
                Console.WriteLine(item.ToString());
            }
            DrawLine();
        }
        public static void Request3Ext()
        {
            Console.WriteLine("Пересечение людей старше 20 и младше 40\t\tExtenssion");
            var ans = collection.Values.SelectMany(stck => stck).Select(person => person).Where(person => person.Age > 20).Intersect
                (collection.Values.SelectMany(stck => stck).Select(person => person).Where(person => person.Age < 40));
            foreach (var item in ans)
            {
                Console.WriteLine(item.ToString());
            }
            DrawLine();
        }

        public static void Request4Linq()
        {
            Console.WriteLine("Средний возраст человека\t\tLinq");
            var ans = (from stck in collection.Values from person in stck select person.Age).Average();
            Console.WriteLine(ans);
            DrawLine();
        }
        public static void Request4Ext()
        {
            Console.WriteLine("Средний возраст человека\t\tExtenssion");
            var ans = collection.Values.SelectMany(stck => stck).Select(person => person.Age).Average(); 
            Console.WriteLine(ans);
            DrawLine();
        }
        public static void Request5Linq()
        {
            Console.WriteLine("Группировка студентов по курсу\t\tLinq");
            var ans = from stck in collection.Values from person in stck where person is Student group person by ((Student)person).Group;
            foreach (IGrouping<string, Person> persons in ans)
            {
                Console.WriteLine($"Cours: {persons.Key}");
                Console.WriteLine("\t{");
                foreach (var item in persons)
                {
                    Console.WriteLine("\t\t" + item.ToString());
                }
                Console.WriteLine("\t}");
            }
            DrawLine();
        }
        public static void Request5Ext()
        {
            Console.WriteLine("Группировка студентов по курсу\t\tExtenssion");
            var ans = collection.Values.SelectMany(stck => stck).Select(person => person).Where(person => person is Student ? true : false).OrderBy(person => (Student)person).GroupBy(person => ((Student)person).Group);
            foreach (IGrouping<string, Person> persons in ans)
            {
                Console.WriteLine($"Cours: {persons.Key}");
                Console.WriteLine("\t{");
                foreach (var item in persons)
                {
                    Console.WriteLine("\t\t" + item.ToString());
                }
                Console.WriteLine("\t}");
            }
            DrawLine();
        }
        public static IEnumerable<T> Select<T>(this MyNewCollection source, Func<Person, T> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                var res = new List<T>();
                foreach (Person item in source)
                {
                    res.Add(selector(item));
                }
                return res;
            }
        }
        public static int Max(this MyNewCollection source)
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                var max = 0;
                foreach (var item in source)
                {
                    if (item.Age > max)
                        max = item.Age;
                }
                return max;
            }
        }


        public static void OrderBy(this MyNewCollection source)
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                QuickSort(source, 0, source.Count() - 1, (Person t1, Person t2) => t1.Age < t2.Age);
            }
        }
        public delegate bool comparator(Person e1, Person e2);
        public static void QuickSort(MyNewCollection source, int leftIndex, int rightIndex, comparator compare)
        {
            var i = leftIndex;
            var j = rightIndex;
            var pivot = source[leftIndex];

            while (i <= j)
            {
                while (compare(source[i], pivot))
                {
                    i++;
                }

                while (compare(pivot, source[j]))
                {
                    j--;
                }

                if (i <= j)
                {
                    var temp = source[i];
                    source[i] = source[j];
                    source[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
            {
                QuickSort(source, leftIndex, j, compare);
            }

            if (i < rightIndex)
            {
                QuickSort(source, i, rightIndex, compare);
            }
        }
        public static void SecondTask()
        {
            MyNewCollection coll = new MyNewCollection("Новая коллекция");
            coll.FillList(10);
            Console.WriteLine("MyNewCollection: " + coll.Name);
            foreach (var item in coll)
            {
                Console.WriteLine(item.ToString());
            }
            DrawLine();

            var maxAge = (from person in coll select person.Age).Max();
            Console.WriteLine($"Наибольший возраст человека: {maxAge}");
            DrawLine();

            Console.WriteLine("Все люди старше 30 лет");
            var ans = from person in coll where person.Age > 30 select person;
            foreach (var item in ans)
            {
                Console.WriteLine(item.ToString());
            }
            DrawLine();

            Console.WriteLine("Сортировка людей по старшенству");
            coll.OrderBy() ;
            foreach (var item in coll)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
