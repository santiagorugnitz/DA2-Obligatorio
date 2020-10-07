using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccess;
using Domain;
using System.Linq;

namespace DataAccessTest
{
    [TestClass]
    public class AdministratorRepositoryTest
    {

        DbContextOptions<TourismContext> options;

        [TestInitialize]
        public void StartUp()
        {
            options = new DbContextOptionsBuilder<TourismContext>()
                             .UseInMemoryDatabase(databaseName: "TestDB")
                             .Options;
        }


        [TestMethod]
        public void AddAdmin()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Administrator>(context);

                var admin = new Administrator()
                {
                    Name = "Bob",
                    Password = "12345678",
                    Email = "bob@mail.com",
                };
                repo.Add(admin);

                Assert.AreEqual("Bob", repo.GetAll().First().Name);
                context.Set<Administrator>().Remove(admin);
                context.SaveChanges();

            }
        }

        [TestMethod]
        public void DeleteAdmin()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Administrator>(context);

                var admin = new Administrator()
                {
                    Name = "Bob",
                    Password = "12345678",
                    Email = "bob@mail.com",
                };
                context.Set<Administrator>().Add(admin);
                context.SaveChanges();
                repo.Delete(admin);

                Assert.AreEqual(0, repo.GetAll().Count());
            }
        }

        [TestMethod]
        public void GetAllAdmins()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Administrator>(context);

                var admin1 = new Administrator()
                {
                    Name = "Bob1",
                    Password = "12345678",
                    Email = "bob1@mail.com",
                };
                var admin2 = new Administrator()
                {
                    Name = "Bob2",
                    Password = "12345678",
                    Email = "bob2@mail.com",
                };
                context.Set<Administrator>().Add(admin1);
                context.Set<Administrator>().Add(admin2);
                context.SaveChanges();

                var res = repo.GetAll();

                Assert.AreEqual(true, res.Contains(admin1));
                Assert.AreEqual(true, res.Contains(admin2));
                Assert.AreEqual(2, res.Count());


                context.Remove(admin1);
                context.Remove(admin2);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void FindAdmins()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new AdministratorRepository(context);

                var admin1 = new Administrator()
                {
                    Name = "Bob1",
                    Password = "12345678",
                    Email = "bob1@mail.com",
                };
                var admin2 = new Administrator()
                {
                    Name = "Bob2",
                    Password = "12345678",
                    Email = "bob2@mail.com",
                };
                context.Set<Administrator>().Add(admin1);
                context.Set<Administrator>().Add(admin2);
                context.SaveChanges();

                var res = repo.Find("bob1@mail.com", "12345678");

                Assert.AreEqual(admin1, res);

                context.Remove(admin1);
                context.Remove(admin2);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void FindInexistingAdmins()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new AdministratorRepository(context);

                var admin1 = new Administrator()
                {
                    Name = "Bob1",
                    Password = "12345678",
                    Email = "bob1@mail.com",
                };
                var admin2 = new Administrator()
                {
                    Name = "Bob2",
                    Password = "12345678",
                    Email = "bob2@mail.com",
                };
                context.Set<Administrator>().Add(admin1);
                context.Set<Administrator>().Add(admin2);
                context.SaveChanges();

                var res = repo.Find("bob3@mail.com", "12345678");

                Assert.AreEqual(null, res);

                context.Remove(admin1);
                context.Remove(admin2);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void FindAdminByToken()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new AdministratorRepository(context);

                var admin1 = new Administrator()
                {
                    Name = "Bob1",
                    Password = "12345678",
                    Email = "bob1@mail.com",
                    Token = "bob1"
                };
                var admin2 = new Administrator()
                {
                    Name = "Bob2",
                    Password = "12345678",
                    Email = "bob2@mail.com",
                };
                context.Set<Administrator>().Add(admin1);
                context.Set<Administrator>().Add(admin2);
                context.SaveChanges();

                var res = repo.Find("bob1");

                Assert.AreEqual(admin1, res);

                context.Remove(admin1);
                context.Remove(admin2);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void FindInexistingAdminByToken()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new AdministratorRepository(context);

                var admin1 = new Administrator()
                {
                    Name = "Bob1",
                    Password = "12345678",
                    Email = "bob1@mail.com",
                    Token = "bob1"
                };
                var admin2 = new Administrator()
                {
                    Name = "Bob2",
                    Password = "12345678",
                    Email = "bob2@mail.com",
                };
                context.Set<Administrator>().Add(admin1);
                context.Set<Administrator>().Add(admin2);
                context.SaveChanges();

                var res = repo.Find("bob3");

                Assert.AreEqual(null, res);

                context.Remove(admin1);
                context.Remove(admin2);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void FindNullToken()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new AdministratorRepository(context);

                var res = repo.Find(null);

                Assert.AreEqual(null, res);

            }
        }
    }
}
