using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestZadanie4.Models;
using TestZadanie4.Models.Db;

namespace TestZadanie4.Services
{
    public class AuthorServices
    {
        ConnectDb _connectDb;
        public AuthorServices(ConnectDb connect)
        {
            _connectDb = connect;
        }

        public async Task<List<Author>> Select()
        {
            return await _connectDb.Authors.Include(a => a.Books).ToListAsync();
        }
        public async Task<BaseResponse<List<Author>>> CreateAuthor(Author author)
        {

            BaseResponse<List<Author>> result = new BaseResponse<List<Author>>();
            try
            {

                using (ConnectDb connectDb = _connectDb)
                {
                    if (author.Books != null)
                    {
                        var books = author.Books.Where(b => b.Checked).ToList();
                        author.Books = null;
                        if (books.Count > 0)
                        {
                            foreach (var item in books)
                            {
                                if (item.Checked == true)
                                {
                                    var book = connectDb.Books.FirstOrDefault(b => b.Id == item.Id);
                                    if (book != null)
                                    {
                                        book.Author = author;
                                    }
                                }



                            }
                        }
                        else
                        {
                            connectDb.Authors.Add(author);
                        }
                    }

                    else
                    {
                        connectDb.Authors.Add(author);
                    }

                    await connectDb.SaveChangesAsync();
                    result.Data = await connectDb.Authors.Include(a => a.Books).ToListAsync();
                }

            }
            catch (Exception ex)
            {
                result.Status = ResultCode.Failure;
                result.ErrorMessage = ex.Message;
            }
            return result;

        }


        public async Task<BaseResponse<List<Author>>> Delete(Guid identificator)
        {
            BaseResponse<List<Author>> result = new BaseResponse<List<Author>>();
            try
            {
                var response = Select().Result.FirstOrDefault(b => b.Id == identificator);
                if (response != null)
                {
                    using (ConnectDb connectDb = _connectDb)
                    {
                        connectDb.Authors.Remove(response);
                        connectDb.SaveChanges();
                        result.Data = await connectDb.Authors.Include(a => a.Books).ToListAsync();
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



        public async Task<BaseResponse<List<Author>>> Update(Author author)
        {
            BaseResponse<List<Author>> result = new BaseResponse<List<Author>>();
            try
            {
                var response = Select().Result.FirstOrDefault(b => b.Id == author.Id);
                if (response != null)
                {
                    using (ConnectDb connectDb = _connectDb)
                    {
                        response.Name = author.Name;
                        response.SurName = author.SurName;
                        response.Birthday = author.Birthday;

                        connectDb.SaveChanges();
                        result.Data = await connectDb.Authors.Include(a => a.Books).ToListAsync();
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
