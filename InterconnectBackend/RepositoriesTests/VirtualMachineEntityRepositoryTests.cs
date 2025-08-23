using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;
using Repositories.Impl;
using TestUtils;

namespace RepositoriesTests
{
    public class VirtualMachineEntityRepositoryTests
    {
        private InterconnectDbContext _context;
        private VirtualMachineEntityRepository _repository;

        [SetUp]
        public void Setup()
        {
            _context = TestMocks.CreateInMemoryDbContext();
            _repository = new VirtualMachineEntityRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task Add_WhenInvoked_ShouldAddEntity()
        {
            await _repository.Add(new VirtualMachineEntityModel
            {
                Id = 0,
                Name = "abc",
                VmUuid = null,
                X = 123,
                Y = 321
            });

            var model = await _context.VirtualMachineEntityModels.Where(m => m.Id == 1).SingleAsync();
            Assert.That(model.Name, Is.EqualTo("abc"));
        }

        [Test]
        public async Task GetAll_WhenInvoked_ShouldGetAllEntities()
        {
            await _context.VirtualMachineEntityModels.AddRangeAsync([
                new VirtualMachineEntityModel
                {
                    Id = 0,
                    Name = "abc",
                    VmUuid = null,
                    X = 123,
                    Y = 321
                },
                new VirtualMachineEntityModel
                {
                    Id = 0,
                    Name = "cba",
                    VmUuid = null,
                    X = 321,
                    Y = 123
                }
               ]);
            await _context.SaveChangesAsync();

            var models = await _repository.GetAll();

            Assert.That(models.Count, Is.EqualTo(2));
            Assert.That(models[0].Name, Is.EqualTo("abc"));
            Assert.That(models[1].Name, Is.EqualTo("cba"));
        }

        [Test]
        public async Task GetById_WhenInvoked_ShouldGetEntityById()
        {
            await _context.VirtualMachineEntityModels.AddAsync(
                new VirtualMachineEntityModel
                {
                    Id = 54,
                    Name = "abc",
                    VmUuid = null,
                    X = 123,
                    Y = 321
                });
            await _context.SaveChangesAsync();

            var model = await _repository.GetById(54);

            Assert.That(model.Name, Is.EqualTo("abc"));
        }

        [Test]
        public async Task Update_WhenInvoked_ShouldUpdateEntity()
        {
            await _context.VirtualMachineEntityModels.AddAsync(
                new VirtualMachineEntityModel
                {
                    Id = 54,
                    Name = "abc",
                    VmUuid = null,
                    X = 123,
                    Y = 321
                });
            await _context.SaveChangesAsync();

            var model = await _context.VirtualMachineEntityModels.Where(m => m.Id == 54).SingleAsync();
            model.Name = "432";
            await _repository.Update(model);

            var updatedModel = await _context.VirtualMachineEntityModels.Where(m => m.Id == 54).SingleAsync();
            Assert.That(updatedModel.Name, Is.EqualTo("432"));
        }
    }
}
