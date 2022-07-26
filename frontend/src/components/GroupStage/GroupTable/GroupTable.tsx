import React from 'react';
import './GroupTable.scss';
import CountryIcon from "../../CountrIcon/CountryIcon";
import Table from 'react-bootstrap/Table';
import 'bootstrap/dist/css/bootstrap.min.css';
import countriesColors from '../../AnimatedLetters/CountriesColors';
import { isMobile } from 'react-device-detect';
import CountryDictShortcuts from '../../YourBets/CountryDictShortcuts';
import ReactCountryFlag from 'react-country-flag';
import CountryDict from '../../YourBets/MyBets/CountryDict';

export interface GroupTableItem {
  name: string,
  points: number,
  won: number,
  drawn: number,
  lost: number,
  group: string,
  goalsFor: number,
  goalsAgainst: number,
}

interface GropuTableProps {
  groupTableData: GroupTableItem[] | null,
  groupTableName: string,
  chosenCountries: { homeCountry: string, awayCountry: string },
  setChosenCountries: React.Dispatch<React.SetStateAction<{
    homeCountry: string;
    awayCountry: string;
  }>>,
}

const GroupTable = ({ groupTableData, groupTableName, chosenCountries, setChosenCountries }: GropuTableProps) => {
  groupTableData?.sort((team2, team1) => team1.points - team2.points ? team1.points - team2.points : (team1.goalsFor - team1.goalsAgainst) - (team2.goalsFor - team2.goalsAgainst))
  return (
    <div className='body-table'>
      <Table style={{ tableLayout: 'fixed' }}>
        <thead >
          <tr>
            <th style={isMobile ? undefined : { width: '5%' }}></th>
            <th style={isMobile ? undefined : { width: '5%', textAlign: 'right' }}> {isMobile ? null : 'Country'}</th>
            <th></th>
            <th>{isMobile ? 'W' : 'Win'}</th>
            <th>{isMobile ? 'L' : 'Loss'}</th>
            <th>{isMobile ? 'D' : 'Draw'}</th>
            <th style={isMobile ? { width: '19%', textAlign: 'left' } : undefined}>{isMobile ? 'G' : 'Goals'}</th>
            <th>{isMobile ? 'P' : 'Points'}</th>
          </tr>
        </thead>
        <tbody className='textCenter'>
          {groupTableData ? groupTableData.map(({ name, points, won, drawn, lost, goalsAgainst, goalsFor }, index) => {
            const mainColor = JSON.parse(countriesColors.get(name as string) as string).mainColor.value
            const secondColor = JSON.parse(countriesColors.get(name as string) as string).secondColor.value
            const thirdColor = JSON.parse(countriesColors.get(name as string) as string).thirdColor.value
            const gradString = {
              backgroundImage: `linear-gradient(to right, rgba${mainColor.slice(0, -1)}, 0.6), rgba${secondColor.slice(0, -1)}, 0.6)`
            }
            return (
              <tr
                key={index}
                onClick={() => setChosenCountries({ homeCountry: name, awayCountry: chosenCountries.awayCountry })}
                style={name === chosenCountries.homeCountry || name === chosenCountries.awayCountry ? gradString : undefined}
              >
                <td style={{ height: '6vh' }}><h5 style={{ paddingTop: '0.4rem' }}>{index + 1}.</h5></td>
                <td style={isMobile ? { textAlign: 'right' } : undefined}>
                  <ReactCountryFlag
                    style={isMobile ? {width: '5vw', height: '4vw'} : {width: '3vw', height: '3vh'}}
                    countryCode={CountryDict.get(name) ? CountryDict.get(name) as string : 'pl'}
                    svg />
                </td>
                {/* <td style={isMobile ? { textAlign: 'right' } : undefined}> <CountryIcon size={isMobile ? 'sm' : 'lg'} countryName={name} />  </td> */}
                <td style={{ fontWeight: '600', textAlign: 'left'}}>
                  {isMobile ? CountryDictShortcuts.get(name) : name}
                </td>
                <td><strong>{won}</strong></td>
                <td><strong>{lost}</strong></td>
                <td><strong>{drawn}</strong></td>
                <td ><strong ><span>{goalsFor}</span> : <span>{goalsAgainst}</span></strong></td>
                <td><strong>{points}</strong></td>
              </tr>
            )
          }) : null}
        </tbody>

      </Table>
    </div>
  )
}

export default GroupTable;
