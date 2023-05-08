using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestZadanie4.Models;
using TestZadanie4.Models.Db;

namespace TestZadanie4.Services
{
    public class BookServices
    {
        ConnectDb _connectDb;
        public BookServices(ConnectDb connect)
        {
            _connectDb = connect;
        }

        public async Task<List<Book>> Select()
        {

            return await _connectDb.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<BaseResponse<List<Book>>> CreateBook(Book book)
        {
            BaseResponse<List<Book>> result = new BaseResponse<List<Book>>();
            try
            {
                List<Book> booksList = new List<Book>();
                using (ConnectDb connectDb = _connectDb)
                {
                    connectDb.Books.Add(book);
                    await connectDb.SaveChangesAsync();
                    result.Data = await _connectDb.Books.Include(b => b.Author).ToListAsync();
                }

            }
            catch (Exception ex)
            {
                result.Status = ResultCode.Failure;
                result.ErrorMessage = ex.Message;
            }
            return result;

        }

        public async Task<BaseResponse<List<Book>>> Delete(Guid identificator)
        {
            BaseResponse<List<Book>> result = new BaseResponse<List<Book>>();
            try
            {
                var response = Select().Result.FirstOrDefault(b => b.Id == identificator);
                if (response != null)
                {
                    using (ConnectDb connectDb = _connectDb)
                    {
                        connectDb.Books.Remove(response);
                        connectDb.SaveChanges();
                        result.Data = await _connectDb.Books.Include(b => b.Author).ToListAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                result.Status = ResultCode.Failure;
                result.ErrorMessage = ex.Message;
            }
            return result;

        }
       
    }
}
