namespace AgroBarn.API.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Version = "v1";
        public const string Base = Version;

        public static class Breeds
        {
            public const string GetAll = Base + "/breeds";

            public const string GetById = Base + "/breeds/{breedId}/id";

            public const string GetByName = Base + "/breeds/{breedName}/name";

            public const string Create = Base + "/breeds";

            public const string Update = Base + "/breeds/{breedId}";

            public const string Low = Base + "/breeds/{breedId}/low";
        }

        public static class Messages
        {
            public const string GetAll = Base + "/messages";

            public const string GetById = Base + "/messages/{messageId}/id";

            public const string GetByCode = Base + "/messages/{messageCode}/code";

            public const string Create = Base + "/messages";

            public const string Update = Base + "/messages/{messageId}";
        }
    }
}
