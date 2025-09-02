using Bogus;

namespace TestBogus
{
    public class FakeDateService
    {
        private readonly Faker<User> _faker;

        public FakeDateService()
        {
            _faker = new Faker<User>("es_MX")
                .RuleFor(x => x.Id, f => f.IndexFaker + 1)
                .RuleFor(x => x.FullName, f => f.Name.FullName())
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.Address, f => f.Address.FullAddress())
                ;
        }

        public User GetUser()
        {
            return _faker.Generate();
        }
    }
}
