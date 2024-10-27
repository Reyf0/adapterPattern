using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adapterPattern
{
    public class ComputerGame
    {
        private string name;
        private PegiAgeRating pegiAgeRating;
        private double budgetInMillionsOfDollars;
        private int minimumGpuMemoryInMegabytes;
        private int diskSpaceNeededInGB;
        private int ramNeededInGb;
        private int coresNeeded;
        private double coreSpeedInGhz;

        public ComputerGame(string name,
                            PegiAgeRating pegiAgeRating,
                            double budgetInMillionsOfDollars,
                            int minimumGpuMemoryInMegabytes,
                            int diskSpaceNeededInGB,
                            int ramNeededInGb,
                            int coresNeeded,
                            double coreSpeedInGhz)
        {
            this.name = name;
            this.pegiAgeRating = pegiAgeRating;
            this.budgetInMillionsOfDollars = budgetInMillionsOfDollars;
            this.minimumGpuMemoryInMegabytes = minimumGpuMemoryInMegabytes;
            this.diskSpaceNeededInGB = diskSpaceNeededInGB;
            this.ramNeededInGb = ramNeededInGb;
            this.coresNeeded = coresNeeded;
            this.coreSpeedInGhz = coreSpeedInGhz;
        }

        public string getName()
        {
            return name;
        }

        public PegiAgeRating getPegiAgeRating()
        {
            return pegiAgeRating;
        }

        public double getBudgetInMillionsOfDollars()
        {
            return budgetInMillionsOfDollars;
        }

        public int getMinimumGpuMemoryInMegabytes()
        {
            return minimumGpuMemoryInMegabytes;
        }

        public int getDiskSpaceNeededInGB()
        {
            return diskSpaceNeededInGB;
        }

        public int getRamNeededInGb()
        {
            return ramNeededInGb;
        }

        public int getCoresNeeded()
        {
            return coresNeeded;
        }

        public double getCoreSpeedInGhz()
        {
            return coreSpeedInGhz;
        }
    }

    public enum PegiAgeRating
    {
        P3, P7, P12, P16, P18
    }

    public class Requirements
    {
        private int gpuGb;
        private int HDDGb;
        private int RAMGb;
        private double cpuGhz;
        private int coresNum;

        public Requirements(int gpuGb,
                            int HDDGb,
                            int RAMGb,
                            double cpuGhz,
                            int coresNum)
        {
            this.gpuGb = gpuGb;
            this.HDDGb = HDDGb;
            this.RAMGb = RAMGb;
            this.cpuGhz = cpuGhz;
            this.coresNum = coresNum;
        }

        public int getGpuGb()
        {
            return gpuGb;
        }

        public int getHDDGb()
        {
            return HDDGb;
        }

        public int getRAMGb()
        {
            return RAMGb;
        }

        public double getCpuGhz()
        {
            return cpuGhz;
        }

        public int getCoresNum()
        {
            return coresNum;
        }
    }

    public interface PCGame
    {
        string getTitle();
        int getPegiAllowedAge();
        bool isTripleAGame();
        Requirements getRequirements();
    }

    public class ComputerGameAdapter : PCGame
    {
        private ComputerGame computerGame;

        public ComputerGameAdapter(ComputerGame computerGame)
        {
            this.computerGame = computerGame;
        }

        public string getTitle()
        {
            return computerGame.getName();
        }

        public int getPegiAllowedAge()
        {
            switch (computerGame.getPegiAgeRating())
            {
                case PegiAgeRating.P3: return 3;
                case PegiAgeRating.P7: return 7;
                case PegiAgeRating.P12: return 12;
                case PegiAgeRating.P16: return 16;
                case PegiAgeRating.P18: return 18;
                default: throw new ArgumentException("Unknown PEGI rating");
            }
        }

        public bool isTripleAGame()
        {
            return computerGame.getBudgetInMillionsOfDollars() > 50;
        }

        public Requirements getRequirements()
        {
            return new Requirements(
                computerGame.getMinimumGpuMemoryInMegabytes() / 1024, 
                computerGame.getDiskSpaceNeededInGB(),
                computerGame.getRamNeededInGb(),
                computerGame.getCoreSpeedInGhz(),
                computerGame.getCoresNeeded()
            );
        }
    }



    internal class Program
    {
        public static void Main()
        {
            // Создаем объект ComputerGame
            ComputerGame cyberpunk = new ComputerGame(
                "Cyberpunk 2077",
                PegiAgeRating.P18,
                70, 
                6 * 1024, 
                70,
                16, 
                4, 
                3.5 
            );

            // Создаем адаптер для ComputerGame
            PCGame adaptedGame = new ComputerGameAdapter(cyberpunk);


            // Используем адаптированный объект
            Console.WriteLine("Игра: " + adaptedGame.getTitle());
            Console.WriteLine("PEGI: " + adaptedGame.getPegiAllowedAge());
            Console.WriteLine("Является ли TripleA?: " + (adaptedGame.isTripleAGame() ? "Да" : "Нет"));

            // Вывод 
            Requirements requirements = adaptedGame.getRequirements();
            Console.WriteLine("Требования:");
            Console.WriteLine("GPU: " + requirements.getGpuGb() + " ГБ");
            Console.WriteLine("HDD: " + requirements.getHDDGb() + " ГБ");
            Console.WriteLine("RAM: " + requirements.getRAMGb() + " ГБ");
            Console.WriteLine("CPU: " + requirements.getCpuGhz() + " ГГц");
            Console.WriteLine("Количество ядер: " + requirements.getCoresNum());
        }
    }
}
