const { Stores } = require('../models/stores-model');
const { Users } = require('../models/users-model');

async function buyCart(req, res) {
    try {
        const { cart } = req.body;
        console.log('cart', cart);

        for (const item of cart) {
            console.log(item.product.id);
            await Stores.findOneAndUpdate( // method of Mongoose 
                { name: item.storeName },
                { $inc: { 'products.$[p].inventory': 1 } }, /// need to fix inventory updating: "Cannot apply $inc to a value of non-numeric type"
                { arrayFilters: [{ 'p.id': item.product.id }] }
            );
        }
        await res.status(201).json({ message: 'Purchased Successfully' });
    } catch (err) {
        await res.status(500).json({
            message: err.message,
        });
    }
}

module.exports = { buyCart };
