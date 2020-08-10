import React from 'react';

export const defaultSorted = [{ dataField: 'id', order: 'asc' }];

const getScoresColumns = () => [
    {
        dataField: 'placeIndex',
        text: 'PLACE',
        sort: true,
    },
    {
        dataField: 'userName',
        text: 'NAME',
        sort: true,
    },
    {
        dataField: 'successCount',
        text: 'SUCCESS SOLUTIONS',
        sort: true,
    },
    {
        dataField: 'successRate',
        text: 'SUCCESS RATE',
        sort: true,
        formatter: (cell) => `${cell * 100} %`,
    },
    {
        dataField: 'taskNames',
        text: 'TASKS',
        sort: true,
        formatter: (cell) => cell.reduce((cellValue, taskName) => `${taskName} `, ''),
    },
];

export default getScoresColumns;
