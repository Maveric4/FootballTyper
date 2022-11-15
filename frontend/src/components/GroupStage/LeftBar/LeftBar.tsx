import React from 'react';
import './LeftBar.scss';
import Table from 'react-bootstrap/Table';
import { BiFootball } from 'react-icons/bi';
import { TbRectangleVertical } from 'react-icons/tb';
import styled, { keyframes } from "styled-components";
import countriesColors from '../../AnimatedLetters/CountriesColors';
export interface LeftBarProps {
    chosenCountries: { homeCountry: string, awayCountry: string },

}

const LeftBar: React.FC<LeftBarProps> = ({ chosenCountries }) => {
    return (
        <LeftBarAnimation>
            <h2 style={{ textAlign: 'center', color: '#DDD'}}>Top Scores</h2>
            <Table>
                <tbody className='groupstage-player-statistics'>
                    {dummyPlayerData.map(({ playerName, goals, assists, team, yellowCards, redCards, imgPath }, index) => {
                        
                        return(
                        <tr style={{textAlign: 'center'}} key={index}>
                            <td style={{fontWeight: '500'}}>{index + 1}</td>
                            <td style={{textAlign: 'left'}}>{playerName}</td>
                            <td><BiFootball size={20} style={{color: '#777'}}/>{goals}</td>
                            <td> <span style={{fontWeight: '500', color: '#CCC'}}>A</span> {assists} </td>
                            <td> <TbRectangleVertical size={20} style={{ color: '#EDED22' }} fill={'#FEFE22'} /> {yellowCards}</td>
                            <td><TbRectangleVertical size={20} style={{ color: '#ED1111' }} fill={'#FE0000'} /> {redCards} </td>
                        </tr>
                    )})}
                </tbody>

            </Table>
        </LeftBarAnimation>
    )
}
export default LeftBar;

//animations

const leftBarAnimation = keyframes`
from{
    transform: translateX(-5rem);
}
to{
    transform: translateX(0rem);
}
`;

const LeftBarAnimation = styled.div`
    animation-name: ${leftBarAnimation};
    animation-duration: 1s;
    display: flex;
    align-items: flex-start !important;
    flex-direction: column;
    padding-top: 5rem;
    padding-left: 1rem;
    animation-timing-function: ease-in-out;
`

export interface Player {
    playerName: string,
    goals: number,
    assists: number,
    team: string,
    yellowCards: number,
    redCards: number,
    imgPath?: string,
}

export const dummyPlayerData: Player[] = [
    {
        playerName: 'Cristiano Ronaldo',
        goals: 0,
        assists: 0,
        team: 'Portugal',
        yellowCards: 0,
        redCards: 0,
        imgPath: 'noPath'
    },
    {
        playerName: 'Leo Messi',
        goals: 0,
        assists: 0,
        team: 'Argentina',
        yellowCards: 0,
        redCards: 0,
        imgPath: 'noPath'
    },
    {
        playerName: 'Neymar Jr',
        goals: 0,
        assists: 0,
        team: 'Brasil',
        yellowCards: 0,
        redCards: 0,
        imgPath: 'noPath'
    },
    {
        playerName: 'Robert Lewandowski',
        goals: 0,
        assists: 0,
        team: 'Poland',
        yellowCards: 0,
        redCards: 0,
        imgPath: 'noPath'
    },
    {
        playerName: 'Karim Benzema',
        goals: 0,
        assists: 0,
        team: 'France',
        yellowCards: 0,
        redCards: 0,
        imgPath: 'noPath'
    },
    {
        playerName: 'Kylian Mbappe',
        goals: 0,
        assists: 0,
        team: 'France',
        yellowCards: 0,
        redCards: 0,
        imgPath: 'noPath'
    },
]