namespace GardenHelperApp.Shared.Constants;

public static class ApiRoutes
{
    public static class Users
    {
        public const string Base = "api/users";
        public const string ById = "api/users/{id}";
        public const string DefaultGarden = "api/gardens/{userId}/default-garden";
        public const string SetDefaultGarden = "api/gardens/{userId}/default-garden/{gardenId}";
    }

    public static class Gardens
    {
        public const string Base = "api/gardens";
        public const string ById = "api/gardens/{id}";
        public const string ByUser = "api/gardens/user/{userId}";
        public const string DefaultGarden = "api/gardens/{userId}/default-garden";
        public const string SetDefaultGarden = "api/gardens/{userId}/default-garden/{gardenId}";
    }

    public static class Locations
    {
        public const string Base = "api/locations";
        public const string ById = "api/locations/{id}";
        public const string ByGarden = "api/locations/garden/{gardenId}";
    }

    public static class Plants
    {
        public const string Base = "api/plants";
        public const string ById = "api/plants/{id}";
        public const string ByGarden = "api/plants/garden/{gardenId}";
        public const string ByLocation = "api/plants/location/{locationId}";
        public const string WithInfo = Base + "/{id}/withinfo";
    }

    public static class PlantInformation
    {
        public const string Base = "api/plantinformation";
        public const string ById = "api/plantinformation/{id}";
    }

    public static class Observations
    {
        public const string Base = "api/observations";
        public const string ById = "api/observations/{id}";
        public const string ByPlant = "api/observations/plant/{plantId}";
        public const string ByUser = "api/observations/user/{userId}";
        public const string Range = "api/observations/range";
        public const string PlantRange = "api/observations/plant/{plantId}/range";
        public const string UserRange = "api/observations/user/{userId}/range";
        public const string ActiveStatus = "api/observations/{id}/active";
    }

    public static class JournalEntries
    {
        public const string Base = "api/journalentries";
        public const string ById = "api/journalentries/{id}";
        public const string ByUser = "api/journalentries/user/{userId}";
        public const string ByGarden = "api/journalentries/garden/{gardenId}";
        public const string Range = "api/journalentries/range";
        public const string UserRange = "api/journalentries/user/{userId}/range";
        public const string GardenRange = "api/journalentries/garden/{gardenId}/range";
    }

    public static class PlantPhotos
    {
        public const string Base = "api/plantphotos";
        public const string ByPlant = "api/plantphotos/plant/{plantId}";
        public const string ById = "api/plantphotos/{photoId}";
    }
}
