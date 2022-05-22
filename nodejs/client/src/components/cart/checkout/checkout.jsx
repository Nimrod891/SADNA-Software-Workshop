import React, { useState } from 'react';
import Radio from '@mui/material/Radio';
import RadioGroup from '@mui/material/RadioGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormControl from '@mui/material/FormControl';
import FormLabel from '@mui/material/FormLabel';
import Button from '@mui/material/Button';
import { fetcher } from '../../../helpers/fetcher';

const CREDIT_CARD = 'credit-card';
export function Checkout({ cart }) {
    const [paymentMethod, setPaymentMethod] = useState(CREDIT_CARD);

    const buyCart = () => {
        console.log('buying cart');
        console.log('cart', cart);
        fetcher('/cart', 'POST', { cart });
    };

    return (
        <div>
            <FormControl>
                <FormLabel id='demo-controlled-radio-buttons-group'>
                    Payment method:
                </FormLabel>
                <RadioGroup
                    aria-labelledby='demo-controlled-radio-buttons-group'
                    name='controlled-radio-buttons-group'
                    value={paymentMethod}
                    onChange={(e) => setPaymentMethod(e.target.value)}
                >
                    <FormControlLabel
                        value={CREDIT_CARD}
                        control={<Radio />}
                        label='Credit Card'
                    />
                    <FormControlLabel
                        value='paypal'
                        control={<Radio />}
                        label='PayPal'
                    />
                </RadioGroup>
                <Button type='submit' variant='contained' onClick={buyCart}>
                    Checkout
                </Button>
            </FormControl>
        </div>
    );
}
