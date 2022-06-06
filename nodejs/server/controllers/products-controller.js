const { ProductDao } = require('../dao/products-dao');

class ProductsController {
    async create(req, res) {
        const product = req.body;
        console.log('product', product);
        const productDao = new ProductDao();
        const newProduct = await productDao.create(product);
        res.send(newProduct);
    }
}

module.exports = {
    ProductsController,
};
