import React, { useState } from 'react';
import { useCart } from '../../context/cart';
import { Checkout } from './checkout/checkout';
import Button from '@mui/material/Button';

export function Cart() {
    const [isCheckout, setIsCheckout] = useState(false);
    const { cart } = useCart();
    console.log('cart', cart);

    return (
        <div>
            <h1>Cart</h1>
            {cart.length > 0 ? (
                <>
                    <div>
                        {cart.map((item) => (
                            <div key={item.product.name}>
                                {item.product.name}
                            </div>
                        ))}
                    </div>
                    <Button
                        type='submit'
                        variant='contained'
                        onClick={() => setIsCheckout(true)}
                    >
                        Checkout
                    </Button>
                </>
            ) : (
                <div>No items in cart</div>
            )}

            {isCheckout && <Checkout cart={cart} />}
        </div>
    );
}
