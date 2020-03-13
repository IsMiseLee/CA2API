using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA2API.Data
{
    public class DogData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DogContext>();
                context.Database.EnsureCreated();
                //context.Database.Migrate();

             
                if (context.Dogs != null && context.Dogs.Any())
                    return;   // DB has already been seeded

                var dogs = GetDogs().ToArray();
                context.Dogs.AddRange(dogs);
                context.SaveChanges();


            }
        }


        public static List<Dog> GetDogs()
        {
            List<Dog> dogs = new List<Dog>() {
                new Dog {ID="1",Name="Rocket",
                    Breed ="Labrador Retriever",
                    Age =1,
                    Information ="The dogs of this breed are very loving, kind, loyal and compassionate  ",
                    ImageURL ="https://www.equipetstores.com/pub/media/catalog/product/cache/afad95d7734d2fa6d0a8ba78597182b7/m/y/my-family-labrador-black-id-tag-2.jpg",
                    IsAdopted =false},
                new Dog {ID="2",Name="Rex",
                    Breed ="German Shepherd",
                    Age =10,
                    Information ="The dogs of this breed are large, agile, muscular dog of noble character and high intelligence. Loyal, confident, courageous, and steady",
                    ImageURL ="https://www.irishexaminer.com/remote/media.central.ie/media/images/i/IzziCapeClearGrandMarshallPaddysDay120319_large.jpg?width=648&s=ie-910457",
                    IsAdopted =false},
                new Dog {ID="3",Name="Mary",
                    Breed ="French Bulldog",
                    Age =3,
                    Information ="The dogs of this breed are playful, alert, adaptable",
                    ImageURL ="https://www.trendingbreeds.com/wp-content/uploads/2019/05/Do-French-Bulldogs-Overheat-large.jpg",
                    IsAdopted =false},
                new Dog {ID="4",Name="Lucky",
                    Breed ="Border Collie",
                    Age =7,
                    Information ="The dogs of this breed are intelligent, extremely energetic, acrobatic and athletic",
                    ImageURL ="https://breedfinder.ikc.ie/Content/images/breeds/140_collie_border.jpg",
                    IsAdopted =false}
            };
            return dogs;
        }

    }


}


