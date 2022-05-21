const { Stores } = require('../models/stores');
const { Users } = require('../models/users');

async function buyCart(req, res) {
    try {
        const { cart } = req.body;
        console.log('cart', cart);

        for (const item of cart) {
            console.log(item.product.id);
            await Stores.findOneAndUpdate(
                { name: item.storeName },
                { $inc: { 'products.$[p].inventory': 1 } }, ///FIXME need to fix inventory updating: "Cannot apply $inc to a value of non-numeric type"
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
