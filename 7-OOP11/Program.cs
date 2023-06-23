namespace OOP11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Animal> animal = new List<Animal>();

            Animal fish = new Fish("Барракуда", 1);

            Animal piranha = fish.Clone("Пиранья", 2);
            Animal pike = fish.Clone("Щука", 3);
            Animal catfish = fish.Clone("Сом", 4);
            Animal salmon = fish.Clone("Лосось", 5);

            animal.Add(fish);
            animal.Add(piranha);
            animal.Add(pike);
            animal.Add(catfish);
            animal.Add(salmon);

            Aquarium aquarium = new Aquarium(animal);

            aquarium.Work();
        }
    }

    class Aquarium
    {
        private List<Animal> _fish;

        public Aquarium(List<Animal> fish)
        {
            _fish = fish;
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

        private void DeleteFish()
        {
            Console.Write("\nВведите название рыбки - ");
            string smallFish = Console.ReadLine();

            if (TryGetFish(out Animal name, smallFish))
            {
                if (TryRemoveFish(name))
                {
                    DescribeResult("Вы убрали рыбку из аквариума.", "Для продолжения нажмите любую клавишу...");

                    SkipTime();
                }
            }
            else
            {
                DescribeResult("Ошибка. Попробуйте ещё раз.", "Для продолжения нажмите любую клавишу...");
            }
        }

        private void MissTime()
        {
            SkipTime();

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
            _fish.Add(new Fish(name, year));

            DescribeResult("Рыбка добавлена.", "Для продолжения нажмите любую клавишу...");
        }

        private void SkipTime()
        {
            for (int i = 0; i < _fish.Count; i++)
            {
                _fish[i].AddYear();
            }

            DescribeResult("Прошло время. Рыбки стали взрослее...", "Для продолжения нажмите любую клавишу...");
        }

        private void RemoveDeadFish()
        {
            for (int i = _fish.Count - 1; i >= 0; i--)
            {
                if (_fish[i].IsDead == false)
                {
                    DescribeResult($"Рыбка {_fish[i].Name} мертва.", "Для продолжения нажмите любую клавишу...");

                    _fish.RemoveAt(i);
                }
            }
        }

        private void ShowFishbowl()
        {
            for (int i = 0; i < _fish.Count; i++)
            {
                _fish[i].ShowInfoWhitebait();
            }
        }

        private bool TryRemoveFish(Animal index)
        {
            return _fish.Remove(index);
        }

        private bool TryGetFish(out Animal fish, string userInput)
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

    abstract class Animal
    {
        private int _maximumYear = 10;

        public Animal(string appellation, int year)
        {
            Name = appellation;
            Age = year;
        }

        public bool IsDead => Age <= _maximumYear;

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

        public abstract Animal Clone(string name, int age);
    }

    class Fish : Animal
    {
        public Fish(string appellation, int year) : base(appellation, year) { }

        public override Animal Clone(string name, int age)
        {
            return new Fish(name, age);
        }
    }
}
