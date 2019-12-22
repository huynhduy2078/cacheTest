using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using TestConnectDB.Database.Redis;
using TestConnectDB.Model;

namespace TestConnectDB.Controllers
{
    [Route("api/[Controller]")]
    public class ProductController : Controller
    {
        private readonly IDistributedCache _cache;
       
        public ProductController(IDistributedCache distributedCache)
        {
            _cache = distributedCache;
        }

        // get/product/name
        [HttpGet("{id}")]
        public async Task<Product> getProduct(String id)
        {
            Product product = new Product();
            try {
                if (!string.IsNullOrEmpty(id))
                {
                    product = await RedisCache.GetObjectAsync<Product>(_cache, id);

                }
             }
            catch(Exception ex)
            {

                throw ex;
            }
            return product;
        }

        //POST api/product
        [HttpPost]
        public async Task<bool> Post([FromBody]Product product)
        {
            bool result = false;

            try { 
                if (product != null)
                {
                    bool isExist = await RedisCache.ExistObjetcAsync<Product>(_cache, product.Id.ToString());
                    if (isExist)
                    {
                        return result;
                    }
                    await RedisCache.SetObjectAsync(_cache, product.Id.ToString(), product);
                    result = true;

                }

            } catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }

        // PUT api/product/5
        [HttpPut("{id}")]
        public async Task<bool> Put(String id,[FromBody]Product product)
        {
            bool result = false;
            try
            {
                if (product != null)
                {
                    bool isExist = await RedisCache.ExistObjetcAsync<Product>(_cache, id);
                    if (isExist)
                    {
                        await RedisCache.SetObjectAsync(_cache, id, product);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        // DELETE api/product/5
        [HttpDelete("{id}")]
        public async Task<bool> Remove(String id, [FromBody]Product product)
        {
            bool result = false;
            try
            {
                if (product != null)
                {
                    bool isExist = await RedisCache.ExistObjetcAsync<Product>(_cache, id);
                    if (isExist)
                    {
                        await RedisCache.RemoveObjectAsync<Product>(_cache, id);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }
}