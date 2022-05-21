const { Router } = require('express');
const cartController = require('../controllers/cart-controller');
const router = Router();

router.post('/', cartController.buyCart);

module.exports = router;
