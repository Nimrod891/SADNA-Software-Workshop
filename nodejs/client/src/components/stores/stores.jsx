import { useEffect, useState } from 'react';
import { fetcher } from '../../helpers/fetcher';
import { useCart } from '../../context/cart';
// import Grid from './grid/grid';
// import Project from './project/project';

export const Stores = () => {
    const [stores, setStores] = useState([]);
    const [selectedStore, setSelectedStore] = useState(null);
    const [basket, setBasket] = useState([]);
    const { addToCart, cart } = useCart();

    useEffect(() => {
        const getStores = async () => {
            const stores = await fetcher('/stores', 'GET');
            setStores(stores);
        };

        getStores();
    }, []);

    const addToBasket = (product) => {
        setBasket((currentBasket) => [...currentBasket, product]);
        addToCart(product, 1, selectedStore.name);
    };
    console.log(cart);

    return (
        <div>
            {!selectedStore ? (
                <div>
                    {stores.map((store) => {
                        return (
                            <div
                                key={store.name}
                                onClick={() => setSelectedStore(store)}
                            >
                                {store.name}
                            </div>
                        );
                    })}
                </div>
            ) : (
                <div>
                    <div>
                        {selectedStore.products.map((product) => {
                            return (
                                <div key={product.name}>
                                    <div>{product.name}</div>
                                    <div>{product.price}</div>
                                    <button
                                        onClick={() => addToBasket(product)}
                                    >
                                        Add to basket
                                    </button>
                                </div>
                            );
                        })}
                        <div>
                            <h1>Current basket</h1>
                            {basket.length > 0 ? (
                                <ul>
                                    {basket.map((product) => {
                                        console.log(product);
                                        return (
                                            <li key={product.name}>
                                                {product.name}
                                            </li>
                                        );
                                    })}
                                </ul>
                            ) : (
                                <div>No items in basket</div>
                            )}
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};
