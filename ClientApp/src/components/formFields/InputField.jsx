import React from 'react';

import { FormField } from './FormField';
import styles from './FormField.module.scss';

// Text Area constants:
const DEFAULT_COUNT_OF_ROWS_IN_TEXT_AREA = 15;
const DEFAULT_MAX_CHARS_OF_TEXT_AREA = 20 * 1000;

// Input Field constants:
const DEFAULT_MAX_CHARS_OF_INPUT_FIELD = 200;

export const InputField = ({ name, isTextarea = false, ...restProps }) =>
    isTextarea ? (
        <FormField name={name}>
            <textarea
                className={styles.inputField}
                rows={DEFAULT_COUNT_OF_ROWS_IN_TEXT_AREA}
                maxLength={DEFAULT_MAX_CHARS_OF_TEXT_AREA}
                {...restProps}
            />
        </FormField>
    ) : (
        <FormField name={name}>
            <input
                className={styles.inputField}
                maxLength={DEFAULT_MAX_CHARS_OF_INPUT_FIELD}
                {...restProps}
            />
        </FormField>
    );
