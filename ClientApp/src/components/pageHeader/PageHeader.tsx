import React from 'react';

interface IPageHeaderProps {
    title?: string;
    subtitle?: string;
}

const PageHeader = ({ title, subtitle }: IPageHeaderProps) => (
    <div style={{ margin: '50px 0' }}>
        {title && <h1 style={{ margin: '20px 0' }}>{title}</h1>}
        {subtitle && <h3>{subtitle}</h3>}
    </div>
);

export default PageHeader;
