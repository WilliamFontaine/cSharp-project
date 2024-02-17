using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Shared;
using Shared.ApiModels;

namespace Test;

[TestClass]
public class MaintenanceTest
{
    [TestMethod]
    public void WorkDescription_Required_Valid()
    {
        var maintenance = new Maintenance { WorkDescription = "Work", VehicleId = 1 };

        var result = ValidateModel(maintenance);

        result.Should().BeEmpty();
    }

    [TestMethod]
    public void WorkDescription_Required_Invalid()
    {
        var maintenance = new Maintenance { WorkDescription = "", VehicleId = 1 };

        var result = ValidateModel(maintenance);

        result.Should().NotBeEmpty();
        result[0].ErrorMessage.Should().Contain("The WorkDescription field is required.");
    }

    [TestMethod]
    public void MaintenanceMileage_Null_Valid()
    {
        var maintenance = new Maintenance { WorkDescription = "Work", VehicleId = 1 };

        var result = ValidateModel(maintenance);

        result.Should().BeEmpty();
    }

    [TestMethod]
    public void MaintenanceMileage_Null_Invalid()
    {
        var maintenance = new Maintenance { WorkDescription = "", VehicleId = 1, MaintenanceMileage = -1 };

        var result = ValidateModel(maintenance);

        result.Should().NotBeEmpty();
        result[0].ErrorMessage.Should().Contain("The field MaintenanceMileage must be between 0 and 2147483647.");
    }

    [TestMethod]
    public void MaintenanceMileage_Valid()
    {
        var maintenance = new Maintenance { WorkDescription = "Work", VehicleId = 1, MaintenanceMileage = 1000 };

        var result = ValidateModel(maintenance);

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