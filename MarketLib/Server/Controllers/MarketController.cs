using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketLib.src.Service;
using MarketLib.src.StoreNS;
using MarketLib.src.UserP;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{

    [ApiController]
    public class MarketController : ControllerBase
    {
        private static IMarketSystemService service = new MarketService();

        [Route("api/Market/connect")]
        [HttpGet]
        public IActionResult connect()
        {

            return new OkObjectResult(service.connect().ErrorMessage);
        }

        [Route("api/Market/login/{connection_id}/{username}/{password}")]
        [HttpGet]
        public IActionResult login(string connection_id, string username, string password)
        {
            if (string.IsNullOrEmpty(connection_id))
                return new BadRequestObjectResult("no conection id");
            if (string.IsNullOrEmpty(username))
                return new BadRequestObjectResult("no username");
            if (string.IsNullOrEmpty(password))
                return new BadRequestObjectResult("no password");
            Response<Subscriber> res = service.login(connection_id, username, password);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);

            return new OkObjectResult(res.Value);
        }

        [Route("api/Market/register/{connection_id}/{username}/{password}")]
        [HttpGet]
        public IActionResult register(string connection_id, string username, string password)
        {
            if (string.IsNullOrEmpty(connection_id))
                return new BadRequestObjectResult("no conection id");
            if (string.IsNullOrEmpty(username))
                return new BadRequestObjectResult("no username");
            if (string.IsNullOrEmpty(password))
                return new BadRequestObjectResult("no password");
            Response res = service.register(connection_id, username, password);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return new OkObjectResult("user is registerd");
        }





        [Route("api/Market/logout/{username}")]
        [HttpGet]
        public IActionResult logout(string username)
        {
            if (string.IsNullOrEmpty(username))
                return new BadRequestObjectResult("no username");

            Response res = service.logout(username);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return new OkObjectResult("logged out succesfully");
        }

        [Route("api/Market/openNewStore/{username}/{store_id}")]
        [HttpGet]
        public IActionResult openNewStore(string username, string store_name)
        {
            if (string.IsNullOrEmpty(username))
                return new BadRequestObjectResult("no username");
            if (string.IsNullOrEmpty(store_name))
                return new BadRequestObjectResult("no username");
            Response res = service.openNewStore(username, store_name);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return new OkObjectResult("shop is now opened");
        }

        [Route("api/Market/addStoreProduct/ ")]
        [HttpGet]
        public IActionResult addStoreProduct(string username, int store_id, string product_name, string category, string sub_category, int quantity, int price)
        {
            if (string.IsNullOrEmpty(username))
                return new BadRequestObjectResult("no username");
            if (store_id < 0)
                return new BadRequestObjectResult("no username");
            Response res = service.addProductToStore(username, store_id, product_name, category, sub_category, quantity, price);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return new OkObjectResult("added item " + product_name + " to the store");
        }


        [Route("api/Market/addItemToBasket/{connection}/{store_id}/{productId}/{amount}")]
        [HttpGet]
        public IActionResult addItemToBasket(string connection, int store_id, int productId, int amount)
        {
            if (string.IsNullOrEmpty(connection))
                return new BadRequestObjectResult("no conection id");
            if (string.IsNullOrEmpty(connection))
                return new BadRequestObjectResult("no store identifier");
            if (productId < 0)
                return new BadRequestObjectResult("no product identifier");
            Response res = service.addItemToBasket(connection, store_id, productId, amount);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok("added item to basket");
        }


        [Route("api/Market/storesInfo/{connection}")]
        [HttpGet]
        public IActionResult storesInfo(string connectionid)
        {
            if (string.IsNullOrEmpty(connectionid))
                return new BadRequestObjectResult("no conection id");
            Response<List<Store>> res = service.storesInfo(connectionid);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok(res.Value);
        }

        [Route("api/Market/searchStore/{connection}/{store_id}")]
        [HttpGet]
        public IActionResult searchStore(string connection, int store_id)
        {
            if (string.IsNullOrEmpty(connection))
                return new BadRequestObjectResult("no conection id");
            if (store_id < 0)
                return new BadRequestObjectResult("no store identifier");
            Response<Store> res = service.searchStore(connection,store_id);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok(res.Value);
        }

        [Route("api/Market/searchProducts/{connection}/{name}")] //TODO :   DDDD
        [HttpGet]
        public IActionResult searchProducts(string connection, string name, string category, int minprice, int maxprice, int product_rating, int store_rating)
        {
            Response < List < Product >> res = service.searchProducts(connection, name, category,minprice,maxprice,product_rating,store_rating);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok(res.Value);

        }

        [Route("api/Market/removeItemFromBasket/{connection}/{storeid}/{productid}")]
        [HttpGet]
        public IActionResult removeItemFromBasket(string connectionID, int storeId, int productId)
        {
            Response res = service.removeItemFromBasket(connectionID, storeId, productId);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok("Product Removed Succefully");
        }

        [Route("api/Market/UpdateItemQuantityBasket/{connection}/{storeid}/{productid}/{amount}")]
        [HttpGet]
        public IActionResult UpdateItemQuantityBasket(string connectionID, int storeId, int productId, int amount)
        {
            Response res = service.UpdateItemQuantityBasket(connectionID, storeId, productId,amount);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok("Updated Product Quantity");
        }


        [Route("api/Market/showCart/{connection}")]
        [HttpGet]
        public IActionResult showCart(string userID)
        {
            Response<List<Basket>> res = service.showCart(userID);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok(res.Value);
        }

        [Route("api/Market/purchaseCart/{connection}")]
        [HttpGet]
        public IActionResult purchaseCart(string userID)
        {
            Response res = service.purchaseCart(userID);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);//todo: dif from different resons of purchase errors.
            return Ok("Purchase Done Succefuly");
        }

        [Route("api/Market/deleteProductFromStore/{username}/{storeid}/{productid}")]
        [HttpGet]
        public IActionResult deleteProductFromStore(string username, int storeId, int productID)
        {
            Response res = service.deleteProductFromStore(username, storeId,productID);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok("Deletion Done Succefuly");
        }

        [Route("api/Market/appointStoreOwner/{username}/{target}/{storeId}")]
        [HttpGet]
        public IActionResult appointStoreOwner(string username, string assigneeUserName, int storeId)
        {
            Response res = service.appointStoreManager(username, assigneeUserName,storeId);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok("Deletion Done Succefuly");
        }

        [Route("api/Market/fireStoreOwner/{username}/{target}/{storeId}")]
        [HttpGet]
        public IActionResult fireStoreOwner(string username, string target, int storeId)
        {
            Response res = service.fireStoreOwner(username, target, storeId);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok($"You have fired {target}");
        }

        [Route("api/Market/appointStoreManager/{username}/{target}/{storeId}")]
        [HttpGet]
        public IActionResult appointStoreManager(string username, string assigneeUserName, int storeId)
        {
            Response res = service.appointStoreManager(username, assigneeUserName, storeId);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok($"You have appointed {assigneeUserName}");
        }

        [Route("api/Market/giveManagerUpdateProductsPermmission/{username}/{storeid}/{manager}")]
        [HttpGet]
        public IActionResult giveManagerUpdateProductsPermmission(string username, int storeId, string managerUserName)
        {
            Response res = service.giveManagerUpdateProductsPermmission(username,  storeId,managerUserName);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok($"permission updated succefuly {managerUserName} ");
        }

        [Route("api/Market/takeManagerUpdatePermmission/{username}/{storeid}/{manager}")]
        [HttpGet]
        public IActionResult takeManagerUpdatePermmission(string username, int storeId, string managerUserName)
        {
            Response res = service.giveManagerUpdateProductsPermmission(username, storeId, managerUserName);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok($"permission removed succefuly {managerUserName} ");
        }

        [Route("api/Market/closeShop/{username}/{storeid}")]
        [HttpGet]
        public IActionResult closeShop(string username, int storeID)
        {
            Response res = service.closeShop(username, storeID);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok($"shop close succefuly ");
        }
        [Route("api/Market/openShop/{username}/{storeid}")]
        [HttpGet]
        public IActionResult openShop(string username, int storeID)
        {
            Response res = service.openShop(username, storeID);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok($"shop Opened succefuly ");
        }

        [Route("api/Market/removeUserFromSystem/{username}/{target_name}")]
        [HttpGet]
        public IActionResult removeUserFromSystem(string username, string target_name)
        {
            Response res = service.removeUserFromSystem(username, target_name);
            if (res.ErrorOccured)
                return BadRequest(res.ErrorMessage);
            return Ok($"shop Opened succefuly ");
        }


        /*



                public Response updateProductDetails(int storeid, string username, int productID, string newSubCategory, int newQuantity, double newPrice)
                {
                    throw new NotImplementedException();
                }

                public Response giveManagerGetHistoryPermmision(string username, int storeId, string managerUserName)
                {
                    throw new NotImplementedException();
                }

                public Response takeManagerGetHistoryPermmision(string username, int storeId, string managerUserName)
                {
                    throw new NotImplementedException();
                }

                public Response removeManager(string username, int storeId, string managerUserName)
                {
                    throw new NotImplementedException();
                }


                public Response<List<string>> showStaffInfo(string username, int storeId)
                {
                    throw new NotImplementedException();
                }

                public Response<List<PurchaseRecord>> getStoreHistory(string username, string storeId)
                {
                    throw new NotImplementedException();
                }

                public Response<List<Subscriber>> getAllUsers(string username)
                {
                    throw new NotImplementedException();
                }
*/
    }






}

