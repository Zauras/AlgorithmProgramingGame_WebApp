import React from 'react';

import styles from './FormField.module.scss';

export const FormField = ({ name, children }) => (
    <div className={styles.formField}>
        <span className={styles.fieldName}>{name}</span>
        <div className={styles.fieldValueContainer}>{children}</div>
    </div>
);
