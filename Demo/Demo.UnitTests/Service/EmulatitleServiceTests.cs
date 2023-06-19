using Demo.DL.Repository.Emulatitle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Demo.BL.Service.Emulatitle;
using AutoMapper;
using NSubstitute.ReturnsExtensions;
using Demo.Common.Execption;
using Demo.DL.Entity;
using Demo.BL.DTO.Emulatitle;

namespace Demo.UnitTests.Service
{
    [TestFixture]
    public class EmulatitleServiceTests
    {
        [Test]
        public async Task GetAsync_NotFound_ReturnException()
        {
            //Arrange
            var id = Guid.Parse("b0b00129-3e91-4168-986e-529983b8e2db");
            
            var emulatitleRepository=Substitute.For<IEmulatitleRepository>();

            emulatitleRepository.GetAsync(id).ReturnsNull();

            var mapper=Substitute.For<IMapper>();

            var emulatitleService= new EmulatitleService(emulatitleRepository, mapper);
            //Act && Assert
            var ex=Assert.ThrowsAsync<NotFoundException>(async () => await emulatitleService.GetAsync(id));
            Assert.That(ex.Message, Is.EqualTo("Không tìm thấy bản ghi"));
        }

        [Test]
        public async Task GetAsync_ValidInput_ReturnEmulatitle()
        {
            //Arrange
            var id = Guid.Parse("b0b00129-3e91-4168-986e-529983b8e2db");

            var emulatitleRepository = Substitute.For<IEmulatitleRepository>();

            var emulatitle = new EmulatitleEntity()
            {
                EmulatitleId = id,
                EmulatitleCode= "Test",
                EmulatitleName= "Test",
            };

            var emulatitleDto = new EmulatitleDto()
            {
                EmulatitleId = id,
                EmulatitleCode = "Test",
                EmulatitleName = "Test",
            };

            emulatitleRepository.GetAsync(id).Returns(emulatitle);

            var mapper = Substitute.For<IMapper>();

            mapper.Map<EmulatitleDto>(emulatitle).Returns(emulatitleDto);

            var emulatitleService = new EmulatitleService(emulatitleRepository, mapper);
            //Act
            
            var actualResult=await emulatitleService.GetAsync(id);

            //Assert
            Assert.That(actualResult, Is.EqualTo(emulatitleDto));
        }

        [Test]
        public async Task DeleteTaskAsync_NotFound_ReturnException()
        {
            //Arrange
            var id = Guid.Parse("b0b00129-3e91-4168-986e-529983b8e2db");

            var emulatitleRepository = Substitute.For<IEmulatitleRepository>();

            emulatitleRepository.GetAsync(id).ReturnsNull();

            var mapper = Substitute.For<IMapper>();

            var emulatitleService = new EmulatitleService(emulatitleRepository, mapper);
            //Act && Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await emulatitleService.DeleteTaskAsync(id));
            Assert.That(ex.Message, Is.EqualTo("Không tìm thấy bản ghi"));
        }

        [Test]
        public async Task DeleteTaskAsync_AllEmulatitleType_ReturnException()
        {
            //Arrange
            var id = Guid.Parse("b0b00129-3e91-4168-986e-529983b8e2db");

            var emulatitleRepository = Substitute.For<IEmulatitleRepository>();

            var emulatitle = new EmulatitleEntity()
            {
                EmulatitleId = id,
                EmulatitleCode = "Test",
                EmulatitleName = "Test",
                EmulatitleType=Common.Enum.EmulatitleType.All,
            };

            emulatitleRepository.GetAsync(id).Returns(emulatitle);

            var mapper = Substitute.For<IMapper>();

            var emulatitleService = new EmulatitleService(emulatitleRepository, mapper);
            //Act && Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await emulatitleService.DeleteTaskAsync(id));
            Assert.That(ex.Message, Is.EqualTo("Không xóa được"));
        }


        [Test]
        public async Task DeleteTaskAsync_ValidInput_DeleteSuccess()
        {
            //Arrange
            var id = Guid.Parse("b0b00129-3e91-4168-986e-529983b8e2db");

            var emulatitleRepository = Substitute.For<IEmulatitleRepository>();

            var emulatitle = new EmulatitleEntity()
            {
                EmulatitleId = id,
                EmulatitleCode = "Test",
                EmulatitleName = "Test",
            };

            emulatitleRepository.GetAsync(id).Returns(emulatitle);

            emulatitleRepository.DeleteAsync(id).Returns(1);

            var mapper = Substitute.For<IMapper>();

            var emulatitleService = new EmulatitleService(emulatitleRepository, mapper);
            //Act

            await emulatitleService.DeleteTaskAsync(id);

            await emulatitleRepository.Received(1).DeleteAsync(id);

            //Assert
            //Assert.That(actualResult, Is.EqualTo(emulatitleDto));
        }

        [Test]
        public async Task CheckCode_NotFound_ReturnBool()
        {
            //Arrange
            var code = "AAAAA";

            var emulatitleRepository = Substitute.For<IEmulatitleRepository>();

            emulatitleRepository.CheckEmulatitleCode(code).Returns(false);

            var mapper = Substitute.For<IMapper>();

            var emulatitleService = new EmulatitleService(emulatitleRepository, mapper);
            //Act

            var actualResult = await emulatitleService.CheckEmulatitleCode(code);

            //Assert
            Assert.That(actualResult, Is.EqualTo(false));
        }

        [Test]
        public async Task CheckCode_Founds_ReturnBool()
        {
            //Arrange
            var code = "AAAAA";

            var emulatitleRepository = Substitute.For<IEmulatitleRepository>();

            var emulatitle = new EmulatitleEntity()
            {
                EmulatitleId = Guid.Parse("b0b00129-3e91-4168-986e-529983b8e2db"),
                EmulatitleCode = "Test",
                EmulatitleName = "Test",
            };

            emulatitleRepository.CheckEmulatitleCode(code).Returns(true);

            var mapper = Substitute.For<IMapper>();

            var emulatitleService = new EmulatitleService(emulatitleRepository, mapper);
            //Act

            var actualResult = await emulatitleService.CheckEmulatitleCode(code);

            //Assert
            Assert.That(actualResult, Is.EqualTo(true));
        }

    }
}
