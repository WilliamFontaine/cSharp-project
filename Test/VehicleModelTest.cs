using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Shared;
using Shared.ApiModels;

namespace Test;

[TestClass]
public class VehicleModelTest
{
    [TestMethod]
    public void Model_MinLength_Valid()
    {
        var vehicleModel = new VehicleModel { Model = "A", Brand = VehicleBrand.Audi, MaintenanceRate = 10000 };

        var result = ValidateModel(vehicleModel);

        result.Should().BeEmpty();
    }

    [TestMethod]
    public void Model_MinLength_Invalid()
    {
        var vehicleModel = new VehicleModel { Model = "", Brand = VehicleBrand.Audi, MaintenanceRate = 10000 };

        var result = ValidateModel(vehicleModel);

        result.Should().NotBeEmpty();
        result[0].ErrorMessage.Should()
            .Contain("The field Model must be a string or array type with a minimum length of '1'.");
    }

    [TestMethod]
    public void MaintenanceRate_Valid()
    {
        var vehicleModel = new VehicleModel { Model = "A", Brand = VehicleBrand.Audi, MaintenanceRate = 10000 };

        var result = ValidateModel(vehicleModel);

        result.Should().BeEmpty();
    }

    [TestMethod]
    public void MaintenanceRate_Invalid()
    {
        var vehicleModel = new VehicleModel { Model = "A", Brand = VehicleBrand.Audi, MaintenanceRate = -1 };

        var result = ValidateModel(vehicleModel);

        result.Should().NotBeEmpty();
        result[0].ErrorMessage.Should().Contain("The field MaintenanceRate must be between 0 and 2147483647.");
    }

    [TestMethod]
    public void MaintenanceRate_Invalid_Zero()
    {
        var vehicleModel = new VehicleModel { Model = "A", Brand = VehicleBrand.Audi, MaintenanceRate = 0 };

        var result = ValidateModel(vehicleModel);

        result.Should().BeEmpty();
    }

    private IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, context, validationResults, true);
        return validationResults;
    }
}