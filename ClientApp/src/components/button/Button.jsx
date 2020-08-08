import React from 'react';
import { Button as BootstrapButton } from 'reactstrap';

export const Button = ({ text, ...restProps }) => (
    <BootstrapButton {...restProps}>{text}</BootstrapButton>
);
