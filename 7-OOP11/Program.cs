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
        private List<Fish> _fishes = new List<Fish>();

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
                    AppendFish();
                }
                else if (userInput == deleteFish)
                {
                    DeleteFish();
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

        private void AppendFish()
        {
            Console.Write("Введите название рыбки - ");
            string name = Console.ReadLine();

            Console.Write("Введите возраст рыбки - ");
            string age = Console.ReadLine();

            if (int.TryParse(age, out int year))
            {
                AddFish(name, year);
            }
            else
            {
                DescribeResult("Ошибка. Попробуйте ещё раз.", "Для продолжения нажмите любую клавишу...");
            }
        }

        private void DeleteFish()
        {
            if (TryGetFish(out Fish fish))
            {
                if (TryRemoveFish(fish))
                {
                    DescribeResult("Вы убрали рыбку из аквариума.", "Для продолжения нажмите любую клавишу...");
                }
            }
            else
            {
                DescribeResult("Ошибка. Попробуйте ещё раз.", "Для продолжения нажмите любую клавишу...");
            }
        }

        private void ShowFishbowl()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                _fishes[i].ShowInfoWhitebait();
            }
        }

        private void MissTime()
        {
            GrowingUpFish();

            RemoveDeadFish();
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
            _fishes.Add(new Fish(name, year));

            DescribeResult("Рыбка добавлена.", "Для продолжения нажмите любую клавишу...");
        }

        private void GrowingUpFish()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                _fishes[i].AddYear();
            }

            DescribeResult("Прошло время. Рыбки стали взрослее...", "Для продолжения нажмите любую клавишу...");
        }

        private void RemoveDeadFish()
        {
            for (int i = _fishes.Count - 1; i >= 0; i--)
            {
                if (_fishes[i].IsDead == false)
                {
                    DescribeResult($"Рыбка {_fishes[i].Name} мертва.", "Для продолжения нажмите любую клавишу...");

                    _fishes.RemoveAt(i);
                }
            }
        }

        private void CreateFish()
        {
            _fishes.Add(new Fish("Пиранья", 1));
            _fishes.Add(new Fish("Щука", 2));
            _fishes.Add(new Fish("Осетр", 3));
            _fishes.Add(new Fish("Вобла", 9));
            _fishes.Add(new Fish("Тунец", 10));
            _fishes.Add(new Fish("Пиранья", 8));
            _fishes.Add(new Fish("Сом", 10));
        }

        private bool TryRemoveFish(Fish index)
        {
            return _fishes.Remove(index);
        }

        private bool TryGetFish(out Fish fish)
        {
            fish = null;

            Console.Write("\nВведите название рыбки - ");
            string name = Console.ReadLine();

            for (int i = 0; i < _fishes.Count; i++)
            {
                if (name.ToLower() == _fishes[i].Name.ToLower())
                {
                    fish = _fishes[i];
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

        public bool IsDead => Age < _maximumYear;

        public string Name { get; private set; }

        public int Age { get; private set; }

        public void ShowInfoWhitebait()
        {
            Console.WriteLine($"{Name} - возраст {Age} лет");
        }

        public void AddYear()
        {
            Age++;
        }
    }
}
