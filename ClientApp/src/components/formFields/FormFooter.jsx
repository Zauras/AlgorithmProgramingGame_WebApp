import React from 'react';

import styles from './FormField.module.scss';

export const FormFooter = ({ children }) => (
    <div className={styles.formField}>
        <span className={styles.fieldName} />
        <div className={styles.fieldValueContainer}>{children}</div>
    </div>
);
