import React from 'react';
import { FormFeedback, Input } from 'reactstrap';
import { isEmpty } from 'lodash';

import { FormField } from './FormField';
import styles from './FormField.module.scss';

// Text Area constants:
const DEFAULT_COUNT_OF_ROWS_IN_TEXT_AREA = 15;
const DEFAULT_MAX_CHARS_OF_TEXT_AREA = 20 * 1000;

// Input Field constants:
const DEFAULT_MAX_CHARS_OF_INPUT_FIELD = 200;

export const InputField = ({ name, isTextarea = false, error, ...restProps }) => (
    <FormField name={name} error={error}>
        {isTextarea ? (
            <Input
                invalid={!isEmpty(error)}
                type='textarea'
                className={styles.inputField}
                rows={DEFAULT_COUNT_OF_ROWS_IN_TEXT_AREA}
                maxLength={DEFAULT_MAX_CHARS_OF_TEXT_AREA}
                {...restProps}
            />
        ) : (
            <Input
                invalid={!isEmpty(error)}
                className={styles.inputField}
                maxLength={DEFAULT_MAX_CHARS_OF_INPUT_FIELD}
                {...restProps}
            />
        )}
        <FormFeedback>{error}</FormFeedback>
    </FormField>
);
