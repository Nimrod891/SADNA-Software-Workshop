import React, { useState } from 'react';

const CartContext = React.createContext(null);

export function CartProvider({ children }) {
    const [cart, setCart] = useState([]);

    const addToCart = async (product, quantity, storeName) => {
        console.log('adding to cart');

        setCart((currentCart) => [
            ...currentCart,
            { product, quantity, storeName },
        ]);
    };

    const value = { addToCart, cart };

    return (
        <CartContext.Provider value={value}>{children}</CartContext.Provider>
    );
}

export function useCart() {
    return React.useContext(CartContext);
}
