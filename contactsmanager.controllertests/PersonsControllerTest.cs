﻿using AutoFixture;
using Moq;
using ServiceContracts;
using Xunit;
using FluentAssertions;
using crudBundle.Controllers;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;

namespace crudTests
{
    public class PersonsControllerTest
    {
        private readonly IPersonsGetterService _personsGetterService;
        private readonly IPersonsAdderService _personsAdderService;
        private readonly IPersonsDeleterService _personsDeleterService;
        private readonly IPersonsSorterService _personsSorterService;
        private readonly IPersonsUpdaterService _personsUpdaterService;

        private readonly ICountriesGetterService _countriesGetterService;

        private readonly ILogger<PersonsController> _logger;

        private readonly Mock<IPersonsGetterService> _personsGetterServiceMock;
        private readonly Mock<IPersonsSorterService> _personsSorterServiceMock;
        private readonly Mock<IPersonsAdderService> _personsAdderServiceMock;
        private readonly Mock<IPersonsDeleterService> _personsDeleterServiceMock;
        private readonly Mock<IPersonsUpdaterService> _personsUpdaterServiceMock;

        private readonly Mock<ICountriesGetterService> _countriesGetterServiceMock;

        private readonly Mock<ILogger<PersonsController>> _loggerMock;

        private readonly IFixture _fixture;
        public PersonsControllerTest()
        {
            _fixture = new Fixture();

            _countriesGetterServiceMock = new Mock<ICountriesGetterService>();

            _personsGetterServiceMock = new Mock<IPersonsGetterService>();
            _personsSorterServiceMock = new Mock<IPersonsSorterService>();
            _personsAdderServiceMock = new Mock<IPersonsAdderService>();
            _personsDeleterServiceMock = new Mock<IPersonsDeleterService>();
            _personsUpdaterServiceMock = new Mock<IPersonsUpdaterService>();

            _loggerMock = new Mock<ILogger<PersonsController>>();

            _countriesGetterService = _countriesGetterServiceMock.Object;

            _personsGetterService = _personsGetterServiceMock.Object;
            _personsAdderService = _personsAdderServiceMock.Object;
            _personsSorterService = _personsSorterServiceMock.Object;
            _personsDeleterService = _personsDeleterServiceMock.Object;
            _personsUpdaterService = _personsUpdaterServiceMock.Object;

            _logger = _loggerMock.Object;
        }

        #region Index
        [Fact]
        public async void Index_ShouldReturnIndexViewWithPersonsList()
        {
            //Arrange
            List<PersonResponse> persons_response_list = _fixture.Create<List<PersonResponse>>();

            PersonsController personsController = new PersonsController(_personsGetterService, _personsAdderService, _personsDeleterService, _personsUpdaterService, _personsSorterService, _countriesGetterService, _logger);

            _personsGetterServiceMock.Setup(temp => temp.GetFilteredPersons(
                It.IsAny<string>(), 
                It.IsAny<string>()))
                .ReturnsAsync(persons_response_list);

            _personsSorterServiceMock.Setup(temp => temp.GetSortedPersons(
                It.IsAny<List<PersonResponse>>(), 
                It.IsAny<string>(), 
                It.IsAny<SortOrderOptions>()))
                .ReturnsAsync(persons_response_list);

            //Act

            IActionResult result = await personsController.Index(
                _fixture.Create<string>(), 
                _fixture.Create<string>(),
                _fixture.Create<string>(), 
                _fixture.Create<SortOrderOptions>());

            //Assert

            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<PersonResponse>>();
            viewResult.ViewData.Model.Should().Be(persons_response_list);
        }


        #endregion

        #region Create

        //[Fact]
        //public async void Create_IfModelErrors_ToReturnCreateView()
        //{
        //    //Arrange
        //    PersonAddRequest person_add_request = _fixture.Create<PersonAddRequest>();

        //    PersonResponse person_response = _fixture.Create<PersonResponse>();

        //    List<CountryResponse> countries = _fixture.Create<List<CountryResponse>>();

        //    _countriesServiceMock.Setup(temp => temp.GetAllCountries()).ReturnsAsync(countries);

        //    _personsAdderServiceMock.Setup(temp => temp.AddPerson(It.IsAny<PersonAddRequest>())).ReturnsAsync(person_response);



        //    PersonsController personsController = new PersonsController(_personsGetterService, _personsAdderService, _personsDeleterService, _personsUpdaterService, _personsSorterService, _countriesService, _logger);


        //    //Act

        //    personsController.ModelState.AddModelError("PersonName", "Person name can't be blank");

        //    IActionResult result = await personsController.Create(person_add_request);

        //    //Assert

        //    ViewResult viewResult = Assert.IsType<ViewResult>(result);

        //    viewResult.ViewData.Model.Should().BeAssignableTo<PersonAddRequest>();

        //    viewResult.ViewData.Model.Should().Be(person_add_request);
        //}

        [Fact]
        public async void Create_IfNoModelErrors_ToReturnRedirectToIndex()
        {
            //Arrange
            PersonAddRequest person_add_request = _fixture.Create<PersonAddRequest>();

            PersonResponse person_response = _fixture.Create<PersonResponse>();

            List<CountryResponse> countries = _fixture.Create<List<CountryResponse>>();

            _countriesGetterServiceMock.Setup(temp => temp.GetAllCountries()).ReturnsAsync(countries);

            _personsAdderServiceMock.Setup(temp => temp.AddPerson(It.IsAny<PersonAddRequest>())).ReturnsAsync(person_response);



            PersonsController personsController = new PersonsController(_personsGetterService, _personsAdderService, _personsDeleterService, _personsUpdaterService, _personsSorterService, _countriesGetterService, _logger);


            //Act
            IActionResult result = await personsController.Create(person_add_request);

            //Assert

            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            redirectResult.ActionName.Should().Be("Index");
        }

        //TODO 

        //Tests for Edit/Delete/CSV/Excel/PDF

        #endregion

        #region Read

        //[Fact]
        //public async void Read_IfNoSqlErrors_ReturnIndex()
        //{
        //    PersonsGetterService persons_get_response = _fixture.Create<PersonsGetterService>();

        //    PersonResponse personResponse = _fixture.Create<PersonResponse>();

        //    List<CountryResponse> countryResponse = _fixture.Create<List<CountryResponse>>();

        //    _countriesGetterServiceMock.Setup(option => option.GetAllCountries().Result);

        //    _personsGetterServiceMock.Setup(option => option.GetAllPersons().Result);

        //    PersonsController personsController = new PersonsController(_personsGetterService, _personsAdderService, _personsDeleterService, _personsUpdaterService, _personsSorterService, _countriesGetterService, _logger) ;
        //}


        #endregion
    }
}
