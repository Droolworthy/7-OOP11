using System;

namespace OOP11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium();

            aquarium.Work();
        }
    }

    class Aquarium
    {
        public void Work()
        {
            Fish fish = new Fish();

            bool isWork = true;

            string skipDay = "1";
            string addFish = "2";
            string deleteFish = "3";
            string exit = "выход";

            while (isWork)
            {
                fish.ShowInfoPisces();

                Console.WriteLine($"\nДля пропуска дня введите - {skipDay}.");
                Console.WriteLine($"\nДобавить рыбку в аквариум введите - {addFish}.");
                Console.WriteLine($"\nУбрать рыбку из аквариума введите - {deleteFish}.");
                Console.WriteLine($"\nВыйти из приложения введите - {exit}.");
                Console.Write("\nВвод: ");
                string userInput = Console.ReadLine();

                if (userInput == addFish)
                {
                    Console.Write("Введите название рыбки - ");
                    string name = Console.ReadLine();

                    Console.Write("Введите возраст рыбки - ");
                    string age = Console.ReadLine();

                    bool isSuccess = int.TryParse(age, out int year);

                    if (isSuccess)
                    {
                        fish.AddPisce(name, year);
                    }
                    else
                    {
                        fish.DescribeResult("Ошибка. Попробуйте ещё раз.", "Для продолжения нажмите любую клавишу...");
                    }
                }
                else if (userInput == deleteFish)
                {
                    Console.Write("\nВведите название рыбки - ");
                    string appellation = Console.ReadLine();

                    if (fish.TryGetPisce(out Fish pisce, appellation))
                    {
                        if (fish.DeletePisce(pisce))
                        {
                            fish.DescribeResult("Вы убрали рыбку из аквариума.", "Для продолжения нажмите любую клавишу...");

                            fish.SkippingTime(out pisce);
                        }
                    }
                    else
                    {
                        fish.DescribeResult("Ошибка. Попробуйте ещё раз.", "Для продолжения нажмите любую клавишу...");
                    }
                }
                else if (userInput == skipDay)
                {
                    if (fish.SkippingTime(out Fish pisce))
                    {
                        fish.DescribeResult("Прошло время. Рыбки стали взрослее...", "Для продолжения нажмите любую клавишу...");
                    }
                    else
                    {
                        fish.DescribeResult($"Рыба - {pisce.Name} мертва.", "Для продолжения нажмите любую клавишу...");

                        fish.DeletePisce(pisce);
                    }
                }
                else if (userInput == exit)
                {
                    isWork = false;
                }
                else
                {
                    fish.DescribeResult($"Ошибка. Введите {addFish}, {deleteFish}, {skipDay} или {exit}.", "Для продолжения нажмите любую клавишу...");
                }
            }
        }
    }

    class Fish
    {
        private List<Fish> _pisces = new List<Fish>();
        private int _maximumYear = 11;

        public Fish(string pisce, int year)
        {
            Name = pisce;
            Age = year;
        }

        public Fish()
        {
            CreatePisce();
        }

        public string Name { get; private set; }

        public int Age { get; private set; }

        public void ShowInfoPisces()
        {
            Console.WriteLine("Рыбы в аквариуме: ");

            for (int i = 0; i < _pisces.Count; i++)
            {
                Console.WriteLine("Имя - " + _pisces[i].Name + ", Здоровье - " + _pisces[i].Age + " лет.");
            }
        }

        public void DescribeResult(string initialDescription, string finalDescription)
        {
            Console.WriteLine(initialDescription);
            Console.WriteLine(finalDescription);
            Console.ReadLine();
            Console.Clear();
        }

        public bool SkippingTime(out Fish fish)
        {
            fish = null;

            int age = 1;
            int pisce = 0;

            for (int i = 0; i < _pisces.Count; i++)
            {
                fish = _pisces[i];
                pisce = _pisces[i].Age += age;
            }

            return pisce < _maximumYear;
        }

        public void AddPisce(string name, int year)
        {
            if (year < _maximumYear)
            {
                _pisces.Add(new Fish(name, year));

                DescribeResult("Рыбка добавлена.", "Для продолжения нажмите любую клавишу...");
            }
            else
            {
                DescribeResult("Ошибка. Попробуйте ещё раз.", "Для продолжения нажмите любую клавишу...");
            }
        }

        public bool DeletePisce(Fish index)
        {
            return _pisces.Remove(index);
        }

        public bool TryGetPisce(out Fish fish, string userInput)
        {
            fish = null;

            for (int i = 0; i < _pisces.Count; i++)
            {
                if (userInput.ToLower() == _pisces[i].Name.ToLower())
                {
                    fish = _pisces[i];
                    return true;
                }
            }

            return false;
        }

        private void CreatePisce()
        {
            _pisces.Add(new Fish(nameof(Barracuda), 1));
            _pisces.Add(new Fish(nameof(Piranha), 2));
            _pisces.Add(new Fish(nameof(Pike), 3));
            _pisces.Add(new Fish(nameof(Catfish), 4));
            _pisces.Add(new Fish(nameof(Salmon), 5));
        }
    }

    class Barracuda : Fish
    {
        public Barracuda(string name, int health) : base(name, health) { }
    }

    class Piranha : Fish
    {
        public Piranha(string name, int health) : base(name, health) { }
    }

    class Pike : Fish
    {
        public Pike(string name, int health) : base(name, health) { }
    }

    class Catfish : Fish
    {
        public Catfish(string name, int health) : base(name, health) { }
    }

    class Salmon : Fish
    {
        public Salmon(string name, int health) : base(name, health) { }
    }
}
