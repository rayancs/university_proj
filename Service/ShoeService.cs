using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using university_proj.DataModels;
using university_proj.DataTransferObj;
using university_proj.DB;

namespace university_proj.Service
{
    public class ShoeService
    {
        private readonly AppDb db;
        public ShoeService (AppDb dbCon)
        {
            this.db = dbCon;
        }
        public async Task<IEnumerable<ShoeModel>> GetAllShoeAsync()
        {
            var res = await db.Shoe.ToListAsync();  
            return res!;

        }
        public async Task <ShoeModel?> GetShoeByID(string id)
        {
            var res = await db.Shoe.Where(x=>x.Id.ToString() == id).FirstOrDefaultAsync();
            if (res == null) return null;
            return res;
        }
        public async Task <IEnumerable<ShoeModel>?> ShoeByType(string size)
        {
            if (size != "small" && size != "medium" && size != "large") return null;
            return await db.Shoe.Where(x=>x.Type == size).ToListAsync();
        }
        public async Task <IEnumerable<ShoeModel>?> GetByAval(string Aval)
        {
            if ( Aval is not "lost" or "stolen" or "free" or "rented") 
            {
                return await db.Shoe.Where(x => x.Status == Aval).ToListAsync();

            }
            return null;
        }
        public async Task<int?> RentShoe(string id)
        {
            if (id == null) return null;
            var shoeAval = await db.Shoe.Where(x => x.SerialNumber == id).FirstOrDefaultAsync();
            if (shoeAval?.Status != "free") { return null; }

            var res = db.Shoe
                .Where(e => e.SerialNumber == id)
                .ExecuteUpdate(_ => 
                _.SetProperty(e => e.Status, "rented")
                .SetProperty(e=>e.usage,shoeAval.usage+1));
            return res;
        }
        public async Task <string> mostPopular(){
            var resSmall = db
            .Shoe
            .Where(x=>x.Status == "rented")
            .Where(y=>y.Type == "small");
            var resMd = db.Sheo

        }
        public async Task<int?> ReturnShoe(string id)
        {
            if (id == null) return null;
            var shoeAval = await db.Shoe.Where(x => x.SerialNumber == id).FirstOrDefaultAsync();
            if (shoeAval?.Status != "rented"){return null;}
            var res = db.Shoe
                .Where(e => e.SerialNumber == id)
                .ExecuteUpdate(_ => _.SetProperty(e => e.Status, "free"));
            return res;
        }
        public async Task<int?> Lost(string id)
        {
            if (id == null) return null;
            var shoeAval = await db.Shoe.Where(x => x.SerialNumber == id).FirstOrDefaultAsync();
            if (shoeAval?.Status == "lost") { return null; }
            var res = db.Shoe
                .Where(e => e.SerialNumber == id)
                .ExecuteUpdate(_ => _.SetProperty(e => e.Status, "lost"));
            return res;
        }


        public async Task<int?> UpdateStatusBySerialNumber(SerialUpdateModel x)
        {
            if(x.SerialId is null )return null;
            if(!(x.Status == "lost" || x.Status == "stolen" || x.Status == "free" || x.Status == "rented"))return null;
            var res =db.Shoe
                 .Where(_ => _.SerialNumber == x.SerialId)
                 .ExecuteUpdate(_ => _.SetProperty(e => e.Status, x.Status));
            db.SaveChanges();
           return res;
        }
        public async Task<IEnumerable<ShoeModel>?> GetBySerial(string x)
        {
            if (x is null) return null;
            var res =  await db.Shoe.Where(i => i.SerialNumber == x).ToListAsync();
            if (res == null) return null;
            return res;   
        }
        public async Task<ShoeModel?> UpdateShoeAsync(ShoeModel shoe)
        {
            // Retrieve the existing shoe from the database based on its Id
            var existingShoe = await db.Shoe.FirstOrDefaultAsync(x => x.Id == shoe.Id);
            // If no shoe with the specified Id is found, return null
            if (existingShoe == null)
            {
                return null;
            }
            // Update the properties of the existing shoe with the values from the provided shoe
            db.Entry(existingShoe).CurrentValues.SetValues(shoe);
            // Save the changes to the database
            await db.SaveChangesAsync();
            return shoe; // Return the updated shoe
        }
    }
}
