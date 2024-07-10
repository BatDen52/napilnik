using System;
using System.IO;

namespace HW3.LogWritter
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePathfinder = new Pathfinder("pathfinder1", new FileLogWritter());
            var consolePathfinder = new Pathfinder("pathfinder2", new ConsoleLogWritter());
            var fridayFilePathfinder = new Pathfinder("pathfinder3", new FridayLogWritter(new FileLogWritter()));
            var fridayConsolePathfinder = new Pathfinder("pathfinder4", new FridayLogWritter(new ConsoleLogWritter()));

            var multiLogger = new MultiLogWritter(new ConsoleLogWritter(), new FridayLogWritter(new FileLogWritter()));
            var multiPathfinder = new Pathfinder("pathfinder5", multiLogger);

            filePathfinder.Find();
            consolePathfinder.Find();
            fridayFilePathfinder.Find();
            fridayConsolePathfinder.Find();
            multiPathfinder.Find();
        }
    }

    class Pathfinder
    {
        private readonly string _name;
        private readonly ILogger _logger;

        public Pathfinder(string name, ILogger logger)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();

            _name = name;

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Find() => _logger.WriteError(_name);
    }

    interface ILogger
    {
        void WriteError(string message);
    }

    class ConsoleLogWritter : ILogger
    {
        public void WriteError(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException();

            Console.WriteLine(message);
        }
    }

    class FileLogWritter : ILogger
    {
        private const string Path = "log.txt";

        public void WriteError(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException();

            File.WriteAllText(Path, message);
        }
    }

    class FridayLogWritter : ILogger
    {
        private readonly ILogger _logger;

        public FridayLogWritter(ILogger logger) =>
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                _logger.WriteError(message);
        }
    }

    class MultiLogWritter : ILogger
    {
        private readonly ILogger[] _loggers;

        public MultiLogWritter(params ILogger[] loggers) =>
            _loggers = loggers ?? throw new ArgumentNullException(nameof(loggers));

        public void WriteError(string message)
        {
            foreach (ILogger logger in _loggers)
                logger.WriteError(message);
        }
    }
}