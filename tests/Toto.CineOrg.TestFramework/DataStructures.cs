using Toto.CineOrg.ServiceModel;

namespace Toto.CineOrg.TestFramework
{
    public static class DataStructures
    {
        public static MovieForCreate ValidMovieForCreate => new MovieForCreate
        {
            Title = RandomGenerator.RandomString(15),
            Description = RandomGenerator.RandomString(55),
            Genre = "romance",
            YearReleased = RandomGenerator.RandomPositiveNumber(2000),
        };
            
        public static MovieForUpdate ValidMovieForUpdate => new MovieForUpdate
        {
            Title = RandomGenerator.RandomString(15),
            Description = RandomGenerator.RandomString(55),
            Genre = "romance",
            YearReleased = RandomGenerator.RandomPositiveNumber(2000),
        };
    }
}