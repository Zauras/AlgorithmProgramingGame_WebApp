import React from 'react';
import { Button as BootstrapButton } from 'reactstrap';

export const Button = ({ text, color, disabled, ...restProps }) => (
    <BootstrapButton color={disabled ? 'secondary' : color} disabled={disabled} {...restProps}>
        {text}
    </BootstrapButton>
);
