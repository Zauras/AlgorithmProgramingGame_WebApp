import React, { useCallback, useEffect, useState } from 'react';
import BootstrapTable from 'react-bootstrap-table-next';
import Select from 'react-select';

import 'react-bootstrap-table-next/dist/react-bootstrap-table2.min.css';

import LoaderScreen from '../../components/loader/LoaderScreen';
import PageHeader from '../../components/pageHeader/PageHeader';

import { requestTopScores } from './scoresApi';
import getScoresColumns, { defaultSorted } from './ScoresColumns';

import styles from './TopScoresList.module.scss';

const topCountOptions = [
    { value: 3, label: 'TOP 3' },
    { value: 5, label: 'TOP 5' },
    { value: 10, label: 'TOP 10' },
    { value: 15, label: 'TOP 15' },
    { value: 25, label: 'TOP 25' },
];

const TopScoresList = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [scoresList, setScoresList] = useState([]);
    const [countOfTopScores, setCountOfTopScores] = useState(3);

    useEffect(() => {
        (async () => fetchItemList())();
    }, [countOfTopScores]);

    const fetchItemList = async () => {
        setIsLoading(true);
        try {
            const response = await requestTopScores(countOfTopScores);
            Boolean(response) ? setScoresList(response) : setScoresList([]);
        } catch (error) {
            setScoresList([]);
        }
        setIsLoading(false);
    };

    const handleTopCountSelection = async ({ value: topCount }) => setCountOfTopScores(topCount);

    const columns = useCallback(getScoresColumns(), []);

    return (
        <div>
            <LoaderScreen dim isLoading={isLoading} />
            <PageHeader title={`TOP ${countOfTopScores} SCORES`} />
            <div className={styles.topCountSelect}>
                <Select
                    defaultValue={topCountOptions[0]}
                    options={topCountOptions}
                    onChange={handleTopCountSelection}
                />
            </div>
            <BootstrapTable
                bootstrap4
                keyField='id'
                data={scoresList}
                columns={columns}
                defaultSorted={defaultSorted}
                sortable
                bordered={false}
                hover
                striped
            />
        </div>
    );
};

export default TopScoresList;
