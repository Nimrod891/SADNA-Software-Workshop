const { Products } = require('../models/products-model');

class ProductDao {
    async create(product) {
        const newProduct = await Products.create(product);
        return newProduct;
    }

    async createMultiple(products) {
        // [ product, product ]
        const newProducts = await Products.insertMany(products);
        return newProducts;
    }

    async getById(id) {
        const product = await Products.findById(id);
        return product;
    }
}

module.exports = { ProductDao };
