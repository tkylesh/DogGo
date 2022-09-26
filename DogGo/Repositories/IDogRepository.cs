using DogGo.Models;
using Newtonsoft.Json.Bson;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();
        Dog GetDogById();
        void AddDog(Dog dog);
        void UpdateDog(Dog dog);
        void DeleteDog(Dog dog);
        List<Dog> GetDogsByOwnerId(int ownerId);
    }
}
