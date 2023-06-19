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
        private List<Fish> _fish = new List<Fish>();

        public Aquarium() 
        {
            CreateFish();
        }

        public void Work()
        {
            bool isWork = true;

            string skipDay = "1";
            string addFish = "2";
            string deleteFish = "3";
            string exit = "выход";

            while (isWork)
            {
                ShowFishbowl();

                Console.WriteLine($"\nДля пропуска дня введите - {skipDay}.");
                Console.WriteLine($"\nДобавить рыбку в аквариум введите - {addFish}.");
                Console.WriteLine($"\nУбрать рыбку из аквариума введите - {deleteFish}.");
                Console.WriteLine($"\nВыйти из приложения введите - {exit}.");
                Console.Write("\nВвод: ");
                string userInput = Console.ReadLine();

                if (userInput == addFish)
                {
                    AppendFish(userInput, addFish);
                }
                else if (userInput == deleteFish)
                {
                    DeleteFish(userInput, deleteFish);
                }
                else if (userInput == skipDay)
                {
                    MissTime();
                }
                else if (userInput == exit)
                {
                    isWork = false;
                }
                else
                {
                    Console.Clear();
                }
            }
        }

        private void AppendFish(string userInput, string addFish)
        {
            if (userInput == addFish)
            {
                Console.Write("Введите название рыбки - ");
                string name = Console.ReadLine();

                Console.Write("Введите возраст рыбки - ");
                string age = Console.ReadLine();

                bool isSuccess = int.TryParse(age, out int year);

                if (isSuccess)
                {
                    AddFish(name, year);
                }
                else
                {
                    DescribeResult("Ошибка. Попробуйте ещё раз.", "Для продолжения нажмите любую клавишу...");
                }
            }
        }

        private void DeleteFish(string userInput, string deleteFish)
        {
            if (userInput == deleteFish)
            {
                Console.Write("\nВведите название рыбки - ");
                string appellation = Console.ReadLine();

                if (TryGetFish(out Fish smallFish, appellation))
                {
                    if (TryDeleteFish(smallFish))
                    {
                        DescribeResult("Вы убрали рыбку из аквариума.", "Для продолжения нажмите любую клавишу...");

                        SkipTime(out smallFish);
                    }
                }
                else
                {
                    DescribeResult("Ошибка. Попробуйте ещё раз.", "Для продолжения нажмите любую клавишу...");
                }
            }
        }

        private void MissTime()
        {
            SkipTime(out Fish fish);

            DescribeResult("Прошло время. Рыбки стали взрослее...", "Для продолжения нажмите любую клавишу...");

            if (TryFindDeadFish(out fish))
            {
                TryDeleteFish(fish);
            }
        }

        private void DescribeResult(string initialDescription, string finalDescription)
        {
            Console.WriteLine(initialDescription);
            Console.WriteLine(finalDescription);
            Console.ReadLine();
            Console.Clear();
        }

        private void AddFish(string name, int year)
        {
            _fish.Add(new Fish(name, year));

            DescribeResult("Рыбка добавлена.", "Для продолжения нажмите любую клавишу...");
        }

        private void SkipTime(out Fish fish)
        {
            fish = null;

            for (int i = 0; i < _fish.Count; i++)
            {
                fish = _fish[i];

                _fish[i].AddYear();
            }
        }

        private void ShowFishbowl()
        {
            for (int i = 0; i < _fish.Count; i++)
            {
                _fish[i].ShowInfoFish();
            }
        }

        private void CreateFish()
        {
            _fish.Add(new Fish(nameof(Barracuda), 1));
            _fish.Add(new Fish(nameof(Piranha), 2));
            _fish.Add(new Fish(nameof(Pike), 3));
            _fish.Add(new Fish(nameof(Catfish), 4));
            _fish.Add(new Fish(nameof(Salmon), 5));
        }

        private bool TryFindDeadFish(out Fish fish)
        {
            fish = null;

            for (int i = 0; i < _fish.Count; i++)
            {
                fish = _fish[i];

                if (_fish[i].IsDead == false)
                {
                    DescribeResult($"Рыбка {fish.Name} мертва.", "Для продолжения нажмите любую клавишу...");

                    return true;
                }
            }

            return false;
        }

        private bool TryDeleteFish(Fish index)
        {
            return _fish.Remove(index);
        }

        private bool TryGetFish(out Fish fish, string userInput)
        {
            fish = null;

            for (int i = 0; i < _fish.Count; i++)
            {
                if (userInput.ToLower() == _fish[i].Name.ToLower())
                {
                    fish = _fish[i];
                    return true;
                }
            }

            return false;
        }
    }

    class Fish
    {
        private int _maximumYear = 10;

        public Fish(string appellation, int year)
        {
            Name = appellation;
            Age = year;
        }

        public bool IsDead => Age <= _maximumYear;

        public string Name { get; private set; }

        public int Age { get; private set; }

        public void ShowInfoFish()
        {
            Console.WriteLine($"{Name} - возраст {Age} лет");
        }

        public void AddYear()
        {
            Age++;
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
