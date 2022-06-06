const { expect } = require('chai');
const { ProductDao } = require('../dao/products-dao');
const { Stores } = require('../models/stores-model');

describe('Products DAO', () => {
    before(async function () {
        this.timeout(60000);

        const { connectToDb } = require('../dao/connect-to-db');
        await connectToDb();
    });

    it('Creates a product', async function () {
        this.timeout(60000);
        const productDao = new ProductDao();
        const newStore = await Stores.create({
            name: 'store1',
            founder: 'user',
        });
        const product = {
            _storeId: newStore._id,
            name: 'phone',
            price: '100',
            imageUrl: ['a'],
            description: 'desc',
            inventory: 10,
        };
        const newProduct = await productDao.create(product);

        expect(newProduct).to.have.property('_id');
        expect(newProduct).to.have.property('_storeId');
        expect(newProduct).to.have.property('name');
        expect(newProduct).to.have.property('price');
        expect(newProduct).to.have.property('imageUrl');
        expect(newProduct.description).to.eq('desc');
        expect(newProduct.inventory).to.eq(10);
    });

    // after()
});
