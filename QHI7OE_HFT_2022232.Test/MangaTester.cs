using Moq;
using NUnit.Framework;
using QHI7OE_HFT_2022232.Logic;
using QHI7OE_HFT_2022232.Models;
using QHI7OE_HFT_2022232.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QHI7OE_HFT_2022232.Test
{
    [TestFixture]
    public class MangaTester
    {
        MangaLogic logic;

        Mock<IRepository<Manga>> mockMangaRepo;

        [SetUp]
        public void Init()
        {
            Author fakeAuthor = new Author()
            {
                AuthorId = 100,
                AuthorName = "Mockitaka Fakeri"
            };
            Genre fakeGenre = new Genre()
            {
                GenreId = 100,
                GenreName = "Fake Mockery"
            };
            var mangas = new List<Manga>()
            {
                new Manga()
                {
                    MangaId = 1,
                    Title = "Fakeo no Mockery",
                    Price = 9,
                    Rating = 0.2,
                    Release = DateTime.Now,
                    AuthorId = 100,
                    GenreId = 100,
                    Author = fakeAuthor,
                    Genre = fakeGenre

                },

                 new Manga()
                {
                    MangaId = 2,
                    Title = "Mockery senpai",
                    Price = 11,
                    Rating = 8.9,
                    Release = DateTime.Now,
                    AuthorId = 100,
                    GenreId = 100,
                    Author = fakeAuthor,
                    Genre = fakeGenre

                },

                  new Manga()
                {
                    MangaId = 3,
                    Title = "Fakera Mockoratary",
                    Price = 7,
                    Rating = 6.3,
                    Release = DateTime.Now,
                    AuthorId = 100,
                    GenreId = 100,
                    Author = fakeAuthor,
                    Genre = fakeGenre

                },
            }.AsQueryable();
            mockMangaRepo = new Mock<IRepository<Manga>>();
            mockMangaRepo.Setup(m => m.ReadAll()).Returns(mangas);
            logic = new MangaLogic(mockMangaRepo.Object);
        }

        [Test]
        public void AVGPriceByAuthorTest()
        {
            var result = logic.AVGPriceByAuthor();

            Assert.That(result.Where(x => x.Key == "Mockitaka Fakeri"), Is.EqualTo(result.Where(x => x.Value == 9)));
        }

        [Test]
        public void AllPriceByYearsTest()
        {
            var results = logic.AllPriceByYears();

            Assert.That(results.Where(x => x.Key == DateTime.Now), Is.EqualTo(results.Where(x => x.Value == 27)));
        }

        [Test]
        public void AVGPriceByYearsTest()
        {
            var results = logic.AVGPriceByYears();

            Assert.That(results.Where(x => x.Key == DateTime.Now), Is.EqualTo(results.Where(x => x.Value == 9)));
        }

        [Test]
        public void AllPriceByGenre()
        {
            var results = logic.AllPriceByGenre();

            Assert.That(results.Where(x => x.Key == "Fake Mockery"), Is.EqualTo(results.Where(x => x.Value == 27)));
        }

        [Test]
        public void AVGPriceByGenreTest()
        {
            var results = logic.AVGPriceByGenre();

            Assert.That(results.Where(x => x.Key == "Fake Mockery"), Is.EqualTo(results.Where(x => x.Value == 9)));
        }

        [Test]
        public void DeleteTest()
        {
            logic.Delete(1);
            mockMangaRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void CreateTest()
        {
            var manga = new Manga() {MangaId = 100, Title = "FakeOneMan", Price= 10, Rating = 5.5, Release = DateTime.Now, AuthorId = 100, GenreId = 100};
            try
            {
                logic.Create(manga);
            }
            catch
            {


            }
            mockMangaRepo.Verify(r => r.Create(manga), Times.Once);

        }
        [Test]
        public void ReadTestException()
        {
            mockMangaRepo
              .Setup(r => r.Read(It.IsAny<int>()))
              .Returns(value: null);

            Assert.Throws<ArgumentException>(() => logic.Read(1));


        }
        [Test]
        public void ReadAllTest()
        {
            var v = logic.ReadAll().Count();

            Assert.That(v, Is.EqualTo(3));
        }
        [Test]
        public void UpdateTest()
        {
            Assert.That(() => logic.Read(0), Throws.TypeOf<ArgumentException>());
            Assert.That(() => logic.Read(4), Throws.TypeOf<ArgumentException>());

        }

    }
}
