namespace Practitci
{
    enum Genre
    {
        Roman = 1,
        Fantasy,
        Detective,
        Poetry
    }

    class Book
    {
        public const int MinYear = 1450;

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public Genre Genre { get; private set; }
        public int Year { get; private set; }
        public decimal Price { get; private set; }

        public Book(int id, string title, string author, Genre genre, int year, decimal price)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Price = price;
        }

        public string GetInfo()
        {
            return $"ID: {Id}, Название: {Title}, Автор: {Author}, Жанр: {Genre}, Год: {Year}, Цена: {Price} руб.";
        }
    }
}