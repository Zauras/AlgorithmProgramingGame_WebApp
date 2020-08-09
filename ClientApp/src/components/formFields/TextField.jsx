import React from 'react';

import { FormField } from './FormField';

export const TextField = ({ name, text }) => (
    <FormField name={name}>
        <span>{text}</span>
    </FormField>
);
