////////////////////////////////////////////////////////////////////////////////// 
//                                                                              //
//      Copyright © 2005-2020 nzsjb                                           //
//                                                                              //
//  This Program is free software; you can redistribute it and/or modify        //
//  it under the terms of the GNU General Public License as published by        //
//  the Free Software Foundation; either version 2, or (at your option)         //
//  any later version.                                                          //
//                                                                              //
//  This Program is distributed in the hope that it will be useful,             //
//  but WITHOUT ANY WARRANTY; without even the implied warranty of              //
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                //
//  GNU General Public License for more details.                                //
//                                                                              //
//  You should have received a copy of the GNU General Public License           //
//  along with GNU Make; see the file COPYING.  If not, write to                //
//  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.       //
//  http://www.gnu.org/copyleft/gpl.html                                        //
//                                                                              //  
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;

using DomainObjects;

namespace EPGCentre
{
    /// <summary>
    /// The form for entering advanced parameters for a frequency.
    /// </summary>
    public partial class AdvancedParameters : Form
    {
        private TuningFrequency frequency;
        private AdvancedRunParameters runParameters;

        /// <summary>
        /// Initialize a new instance of the AdvancedParameters class.
        /// </summary>
        public AdvancedParameters()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize the form.
        /// </summary>
        /// <param name="frequency">The frequency the parameters apply to.</param>
        public void Initialize(TuningFrequency frequency)
        {
            this.frequency = frequency;
            this.runParameters = frequency.AdvancedRunParamters;

            this.Text += frequency.ToString();

            cbUseContentSubtype.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.UseContentSubtype);
            
            cbUseFreeSatTables.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.UseFreeSatTables);
            if (!cbUseFreeSatTables.Checked)
            {
                if (frequency.Provider != null && frequency.Provider.Options != null)
                    cbUseFreeSatTables.Checked = OptionEntry.IsDefined(frequency.Provider.Options, OptionName.UseFreeSatTables);
            }

            cbCustomCategoriesOverride.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.CustomCategoryOverride);
            cbProcessAllStations.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.ProcessAllStations);
            cbUseStationLogos.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.UseImage);
            cbCreateChannels.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.CreateMissingChannels);
            cbSidMatchOnly.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.SidMatchOnly);
                        
            if (OptionEntry.IsDefined(runParameters.Options, OptionName.FormatReplace))
                cboEITFormatting.Text = cboEITFormatting.Items[1].ToString();
            else
            {
                if (OptionEntry.IsDefined(runParameters.Options, OptionName.FormatConvert))
                    cboEITFormatting.Text = cboEITFormatting.Items[2].ToString();
                else
                {
                    if (OptionEntry.IsDefined(runParameters.Options, OptionName.FormatConvertTable) || runParameters.ByteConvertTable != null)
                        cboEITFormatting.Text = cboEITFormatting.Items[3].ToString();
                    else
                        cboEITFormatting.Text = cboEITFormatting.Items[0].ToString();
                }
            }

            cboConvertTables.Items.Clear();
            cboConvertTables.Items.Add("Not used");

            Collection<string> tableNames = ByteConvertFile.GetTableNameList();
            foreach (string tableName in tableNames)
                cboConvertTables.Items.Add(tableName);

            if (runParameters.ByteConvertTable == null)
                cboConvertTables.Text = cboConvertTables.Items[0].ToString();
            else
            {
                bool found = false;

                foreach (string item in cboConvertTables.Items)
                {
                    if (item == runParameters.ByteConvertTable)
                    {
                        cboConvertTables.Text = item;
                        found = true;
                        break;
                    }
                }

                if (!found)
                    cboConvertTables.Text = cboConvertTables.Items[0].ToString();
            }

            cboCarouselProfiles.Items.Clear();
            cboCarouselProfiles.Items.Add("Not used");

            Collection<string> profileNames = EITCarouselFile.GetNameList();
            foreach (string profileName in profileNames)
                cboCarouselProfiles.Items.Add(profileName);

            if (runParameters.EITCarousel == null)
                cboCarouselProfiles.Text = cboCarouselProfiles.Items[0].ToString();
            else
            {
                bool found = false;

                foreach (string item in cboCarouselProfiles.Items)
                {
                    if (item == runParameters.EITCarousel)
                    {
                        cboCarouselProfiles.Text = item;
                        found = true;
                        break;
                    }
                }

                if (!found)
                    cboCarouselProfiles.Text = cboCarouselProfiles.Items[0].ToString();
            }

            dudEPGDays.Items.Add("All");
            for (int dayNumber = 1; dayNumber < 32; dayNumber++)
                dudEPGDays.Items.Add(dayNumber.ToString());
            if (runParameters.EPGDays == -1)
                dudEPGDays.SelectedIndex = 0;
            else
                dudEPGDays.SelectedIndex = runParameters.EPGDays;

            if (OptionEntry.IsDefined(runParameters.Options, OptionName.UseDescAsCategory))
                cboUseDescriptionAs.SelectedIndex = 1;
            else
            {
                if (OptionEntry.IsDefined(runParameters.Options, OptionName.UseDescAsSubtitle))
                    cboUseDescriptionAs.SelectedIndex = 2;
                else
                {
                    if (OptionEntry.IsDefined(runParameters.Options, OptionName.UseNoDesc))
                        cboUseDescriptionAs.SelectedIndex = 3;
                    else
                        cboUseDescriptionAs.SelectedIndex = 0;
                }
            }

            foreach (Country country in Country.Load())
                cboLocation.Items.Add(country);

            if (runParameters.CountryCode != null)
            {
                Country country = Country.FindCountryCode(runParameters.CountryCode, Country.Load());
                if (country != null)
                    cboLocation.SelectedItem = country;
                else
                    cboLocation.SelectedIndex = 0;
            }
            else
            {
                if (frequency.CountryCode != null)
                {
                    Country country = Country.FindCountryCode(frequency.CountryCode, Country.Load());
                    if (country != null)
                        cboLocation.SelectedItem = country;
                    else
                        cboLocation.SelectedIndex = 0;
                }
                else
                {
                    if (frequency.Provider != null && frequency.Provider.CountryCode != null)
                    {
                        Country country = Country.FindCountryCode(frequency.Provider.CountryCode, Country.Load());
                        if (country != null)
                            cboLocation.SelectedItem = country;
                        else
                            cboLocation.SelectedIndex = 0;
                    }
                    else
                        cboLocation.SelectedIndex = 0;
                }
            }
            cboLocation_SelectedIndexChanged(null, null);

            foreach (CharacterSet characterSet in CharacterSet.CharacterSets)
                cboCharacterSet.Items.Add(characterSet);
            if (runParameters.CharacterSet != null)
                cboCharacterSet.Text = CharacterSet.FindCharacterSet(runParameters.CharacterSet).ToString();
            else
                cboCharacterSet.SelectedIndex = 0;

            if (!OptionEntry.IsDefined(runParameters.Options, OptionName.UseBroadcastCp))
                cboCharacterSetPriority.SelectedIndex = 0;
            else
                cboCharacterSetPriority.SelectedIndex = 1;

            foreach (LanguageCode languageCode in LanguageCode.LanguageCodeList)
                cboInputLanguage.Items.Add(languageCode);
            if (runParameters.InputLanguage != null)
                cboInputLanguage.Text = LanguageCode.FindLanguageCode(runParameters.InputLanguage).ToString();
            else
                cboInputLanguage.SelectedIndex = 0;

            if (runParameters.SDTPid != -1)
                nudSDTPid.Value = runParameters.SDTPid;
            else
                nudSDTPid.Value = 0;

            if (runParameters.EITPid != -1)
                nudEITPid.Value = runParameters.EITPid;
            else
                nudEITPid.Value = 0;

            if (runParameters.MHW1Pids != null)
            {
                nudMHW1Pid1.Value = runParameters.MHW1Pids[0];
                nudMHW1Pid2.Value = runParameters.MHW1Pids[1];
            }
            else
            {
                nudMHW1Pid1.Value = 0;
                nudMHW1Pid2.Value = 0;
            }

            if (runParameters.MHW2Pids != null)
            {
                nudMHW2Pid1.Value = runParameters.MHW2Pids[0];
                nudMHW2Pid2.Value = runParameters.MHW2Pids[1];
                nudMHW2Pid3.Value = runParameters.MHW2Pids[2];
            }
            else
            {
                nudMHW2Pid1.Value = 0;
                nudMHW2Pid2.Value = 0;
                nudMHW2Pid3.Value = 0;
            }

            if (runParameters.DishNetworkPid != -1)
                nudDishNetworkPid.Value = runParameters.DishNetworkPid;
            else
                nudDishNetworkPid.Value = 0;

            switch (frequency.CollectionType)
            {
                case CollectionType.BellTV:
                    cbUseContentSubtype.Checked = false;
                    cbUseContentSubtype.Enabled = false;                    
                    cbUseFreeSatTables.Checked = false;
                    cbUseFreeSatTables.Enabled = false;
                    cbCustomCategoriesOverride.Enabled = true;
                    cbUseStationLogos.Checked = false;
                    cbUseStationLogos.Enabled = false;
                    cbCreateChannels.Enabled = true;
                    cbSidMatchOnly.Checked = false;
                    cbSidMatchOnly.Enabled = false;
                    setTextFormatFields(false);
                    setCarouselProfile(false);                    
                    setLocationFields(false, false);
                    setCharacterSetFields(true);
                    setPidFields(false, false, false, false);
                    break;
                case CollectionType.DishNetwork:
                    cbUseContentSubtype.Enabled = true;
                    cbUseFreeSatTables.Checked = false;
                    cbUseFreeSatTables.Enabled = false;
                    cbCustomCategoriesOverride.Enabled = true;
                    cbUseStationLogos.Checked = false;
                    cbUseStationLogos.Enabled = false;
                    cbCreateChannels.Enabled = true;
                    cbSidMatchOnly.Checked = false;
                    cbSidMatchOnly.Enabled = false;
                    setTextFormatFields(false);
                    setCarouselProfile(false); 
                    setLocationFields(false, false);
                    setCharacterSetFields(false);
                    setPidFields(false, false, false, true);
                    break;
                case CollectionType.EIT:
                    cbUseContentSubtype.Enabled = true;                    
                    cbUseFreeSatTables.Enabled = true;
                    cbCustomCategoriesOverride.Enabled = true;
                    cbUseStationLogos.Checked = false;
                    cbUseStationLogos.Enabled = false;
                    cbCreateChannels.Enabled = true;
                    cbSidMatchOnly.Enabled = true;
                    setTextFormatFields(true);
                    setCarouselProfile(true); 
                    setCharacterSetFields(true);
                    setPidFields(true, false, false, false);
                    break;
                case CollectionType.FreeSat:
                    cbUseContentSubtype.Enabled = true;
                    cbUseFreeSatTables.Checked = false;
                    cbUseFreeSatTables.Enabled = false;
                    cbCustomCategoriesOverride.Enabled = true;
                    cbUseStationLogos.Checked = false;
                    cbUseStationLogos.Enabled = false;
                    cbCreateChannels.Checked = false;
                    cbCreateChannels.Enabled = false;
                    cbSidMatchOnly.Checked = false;
                    cbSidMatchOnly.Enabled = false;
                    setTextFormatFields(false);
                    setCharacterSetFields(false);
                    setCarouselProfile(false); 
                    setPidFields(false, false, false, false);
                    break;
                case CollectionType.MediaHighway1:
                    cbUseContentSubtype.Checked = false;
                    cbUseContentSubtype.Enabled = false;
                    cbUseFreeSatTables.Checked = false;
                    cbUseFreeSatTables.Enabled = false;
                    cbCustomCategoriesOverride.Enabled = true;
                    cbUseStationLogos.Checked = false;
                    cbUseStationLogos.Enabled = false;
                    cbCreateChannels.Checked = false;
                    cbCreateChannels.Enabled = false;
                    cbSidMatchOnly.Checked = false;
                    cbSidMatchOnly.Enabled = false;
                    setTextFormatFields(false);
                    setCharacterSetFields(false);
                    setCarouselProfile(false); 
                    setPidFields(false, true, false, false);
                    break;
                case CollectionType.MediaHighway2:
                    cbUseContentSubtype.Checked = false;
                    cbUseContentSubtype.Enabled = false;
                    cbUseFreeSatTables.Checked = false;
                    cbUseFreeSatTables.Enabled = false;
                    cbCustomCategoriesOverride.Enabled = true; 
                    cbUseStationLogos.Checked = false;
                    cbUseStationLogos.Enabled = false;
                    cbCreateChannels.Checked = false;
                    cbCreateChannels.Enabled = false;
                    cbSidMatchOnly.Checked = false;
                    cbSidMatchOnly.Enabled = false;
                    setTextFormatFields(false);
                    setCharacterSetFields(false);
                    setCarouselProfile(false); 
                    setPidFields(false, false, true, false);
                    break;
                case CollectionType.MHEG5:
                    cbUseContentSubtype.Checked = false;
                    cbUseContentSubtype.Enabled = false;
                    cbUseFreeSatTables.Checked = false;
                    cbUseFreeSatTables.Enabled = false;
                    cbCustomCategoriesOverride.Checked = false;
                    cbCustomCategoriesOverride.Enabled = false;
                    cbUseStationLogos.Enabled = true;
                    cbCreateChannels.Checked = false;
                    cbCreateChannels.Enabled = false;
                    cbSidMatchOnly.Checked = false;
                    cbSidMatchOnly.Enabled = false;
                    setTextFormatFields(false);
                    setCharacterSetFields(false);
                    setCarouselProfile(false); 
                    setPidFields(false, false, false, false);
                    break;
                case CollectionType.OpenTV:
                    cbUseContentSubtype.Checked = false;
                    cbUseContentSubtype.Enabled = false;
                    cbUseFreeSatTables.Checked = false;
                    cbUseFreeSatTables.Enabled = false;
                    cbCustomCategoriesOverride.Enabled = true;
                    cbUseStationLogos.Checked = false;
                    cbUseStationLogos.Enabled = false;
                    cbCreateChannels.Checked = false;
                    cbCreateChannels.Enabled = false;
                    cbSidMatchOnly.Checked = false;
                    cbSidMatchOnly.Enabled = false;
                    setTextFormatFields(false);
                    setCharacterSetFields(false);
                    setCarouselProfile(false); 
                    setPidFields(false, false, false, false);
                    break;
                case CollectionType.PSIP:
                    cbUseContentSubtype.Checked = false;
                    cbUseContentSubtype.Enabled = false;
                    cbUseFreeSatTables.Checked = false;
                    cbUseFreeSatTables.Enabled = false;
                    cbCustomCategoriesOverride.Enabled = true;
                    cbUseStationLogos.Checked = false;
                    cbUseStationLogos.Enabled = false;
                    cbCreateChannels.Checked = false;
                    cbCreateChannels.Enabled = false;
                    cbSidMatchOnly.Checked = false;
                    cbSidMatchOnly.Enabled = false;
                    setTextFormatFields(false);
                    setLocationFields(false, false);
                    setCharacterSetFields(false);
                    setCarouselProfile(false); 
                    setPidFields(false, false, false, false);
                    break;
                case CollectionType.SiehfernInfo:
                    cbUseContentSubtype.Checked = false;
                    cbUseContentSubtype.Enabled = false;
                    cbUseFreeSatTables.Checked = false;
                    cbUseFreeSatTables.Enabled = false;
                    cbCustomCategoriesOverride.Enabled = true;
                    cbUseStationLogos.Checked = false;
                    cbUseStationLogos.Enabled = false;
                    cbCreateChannels.Checked = false;
                    cbCreateChannels.Enabled = false;
                    cbSidMatchOnly.Checked = false;
                    cbSidMatchOnly.Enabled = false;
                    setTextFormatFields(false);
                    setLocationFields(false, false);
                    setCharacterSetFields(false);
                    setCarouselProfile(false); 
                    setPidFields(false, false, false, false);
                    break;
                case CollectionType.NDS:
                    cbUseContentSubtype.Checked = false;
                    cbUseContentSubtype.Enabled = false;
                    cbUseFreeSatTables.Checked = false;
                    cbUseFreeSatTables.Enabled = false;
                    cbCustomCategoriesOverride.Enabled = true;
                    cbUseStationLogos.Checked = false;
                    cbUseStationLogos.Enabled = false;
                    cbCreateChannels.Checked = false;
                    cbCreateChannels.Enabled = false;
                    cbSidMatchOnly.Checked = false;
                    cbSidMatchOnly.Enabled = false;
                    setTextFormatFields(false);
                    setLocationFields(false, false);
                    setCharacterSetFields(false);
                    setCarouselProfile(true);
                    setPidFields(false, false, false, false);
                    break;
                default:
                    break;
            }
        }

        private void setTextFormatFields(bool enabled)
        {
            if (enabled)
            {
                cboEITFormatting.Enabled = true;
                lblTextFormatting.Enabled = true;

                lblByteConversion.Enabled = true;
                if (cboEITFormatting.SelectedIndex == 3)
                    cboConvertTables.Enabled = true;
                else
                {
                    cboConvertTables.SelectedIndex = 0;
                    cboConvertTables.Enabled = false;
                }

                cboUseDescriptionAs.Enabled = true;
                lblUseDescriptionAs.Enabled = true;
            }
            else
            {
                cboEITFormatting.Enabled = false;
                cboEITFormatting.SelectedIndex = 0;
                lblTextFormatting.Enabled = false;
                
                cboConvertTables.Enabled = false;
                cboConvertTables.SelectedIndex = 0;
                lblByteConversion.Enabled = false;

                cboUseDescriptionAs.Enabled = false;
                cboUseDescriptionAs.SelectedIndex = 0;
                lblUseDescriptionAs.Enabled = false;
            }
        }

        private void setLocationFields(bool countryEnabled, bool areaRegionEnabled)
        {
            if (countryEnabled)
            {
                cboLocation.Enabled = true;
                lblLocation.Enabled = true;
            }
            else
            {
                cboLocation.Enabled = false;
                cboLocation.SelectedIndex = -1;
                lblLocation.Enabled = false;                
            }

            if (areaRegionEnabled)
            {
                cboLocationArea.Enabled = true;
                lblLocationArea.Enabled = true;
                
                cboLocationRegion.Enabled = true;
                lblLocationRegion.Enabled = true;
            }
            else
            {
                cboLocationArea.Enabled = false;
                cboLocationArea.SelectedIndex = -1;
                lblLocationArea.Enabled = false;
                
                cboLocationRegion.Enabled = false;
                cboLocationRegion.SelectedIndex = -1;
                lblLocationRegion.Enabled = false;
            }

            if (countryEnabled || areaRegionEnabled)
                gpLocationInformation.Enabled = true;
            else
                gpLocationInformation.Enabled = false;
        }

        private void setCharacterSetFields(bool enabled)
        {
            if (enabled)
            {
                cboCharacterSet.Enabled = true;
                lblCharacterSet.Enabled = true;

                cboCharacterSetPriority.Enabled = true;
                lblCharacterSetPriority.Enabled = true;

                cboInputLanguage.Enabled = true;
                lblInputLanguage.Enabled = true;
            }
            else
            {
                cboCharacterSet.SelectedIndex = 0;
                cboCharacterSet.Enabled = false;
                lblCharacterSet.Enabled = false;
                
                cboCharacterSetPriority.SelectedIndex = 0;
                cboCharacterSetPriority.Enabled = false;
                lblCharacterSetPriority.Enabled = false;
                
                cboInputLanguage.SelectedIndex = 0;
                cboInputLanguage.Enabled = false;
                lblInputLanguage.Enabled = false;
            }
        }

        private void setPidFields(bool eitEnabled, bool mhw1Enabled, bool mhw2Enabled, bool dishNetworkEnabled)
        {
            if (eitEnabled)
            {
                lblEIT.Enabled = true;
                nudEITPid.Enabled = true;
            }
            else
            {
                lblEIT.Enabled = false;
                nudEITPid.Value = 0;
                nudEITPid.Enabled = false;
            }

            if (mhw1Enabled)
            {
                lblMHW1.Enabled = true;
                nudMHW1Pid1.Enabled = true;
                nudMHW1Pid2.Enabled = true;
            }
            else
            {
                lblMHW1.Enabled = false;

                nudMHW1Pid1.Value = 0;
                nudMHW1Pid1.Enabled = false;

                nudMHW1Pid2.Value = 0;
                nudMHW1Pid2.Enabled = false;
            }

            if (mhw2Enabled)
            {
                lblMHW2.Enabled = true;
                nudMHW2Pid1.Enabled = true;
                nudMHW2Pid2.Enabled = true;
                nudMHW2Pid3.Enabled = true;
            }
            else
            {
                lblMHW2.Enabled = false;

                nudMHW2Pid1.Value = 0;
                nudMHW2Pid1.Enabled = false;

                nudMHW2Pid2.Value = 0;
                nudMHW2Pid2.Enabled = false;

                nudMHW2Pid3.Value = 0;
                nudMHW2Pid3.Enabled = false;
            }

            if (dishNetworkEnabled)
            {
                lblDishNetwork.Enabled = true;
                nudDishNetworkPid.Enabled = true;
            }
            else
            {
                lblDishNetwork.Enabled = false;
                nudDishNetworkPid.Value = 0;
                nudDishNetworkPid.Enabled = false;
            }

            if (eitEnabled || mhw1Enabled || mhw2Enabled || dishNetworkEnabled)
            {
                gpCustomPids.Enabled = true;
                cbHexPids.Enabled = true;
            }
            else
            {
                gpCustomPids.Enabled = false;
                cbHexPids.Checked = false;
                cbHexPids.Enabled = false;
            }
        }

        private void setCarouselProfile(bool enabled)
        {
            lblCarouselProfile.Enabled = enabled;
            cboCarouselProfiles.Enabled = enabled;
            
            if (!enabled)
                cboCarouselProfiles.SelectedIndex = 0;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (cboEITFormatting.Text == cboEITFormatting.Items[3].ToString() && cboConvertTables.SelectedIndex == 0)
            {
                MessageBox.Show("No byte conversion table selected when formatting option set to use table.",
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cboLocation.Enabled && frequency.CollectionType == CollectionType.OpenTV && cboLocation.SelectedIndex == 0)
            {
                MessageBox.Show("No country selected.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            runParameters.Options = new Collection<OptionEntry>();

            if (cbUseContentSubtype.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.UseContentSubtype));
            if (cbUseFreeSatTables.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.UseFreeSatTables));
            if (cbCustomCategoriesOverride.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.CustomCategoryOverride));
            if (cbProcessAllStations.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.ProcessAllStations));
            if (cbUseStationLogos.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.UseImage));
            if (cbCreateChannels.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.CreateMissingChannels));
            if (cbSidMatchOnly.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.SidMatchOnly)); 
                        
            if (cboEITFormatting.Text == cboEITFormatting.Items[1].ToString())
                runParameters.Options.Add(new OptionEntry(OptionName.FormatReplace));
            else
            {
                if (cboEITFormatting.Text == cboEITFormatting.Items[2].ToString())
                    runParameters.Options.Add(new OptionEntry(OptionName.FormatConvert));
                else
                {
                    if (cboEITFormatting.Text == cboEITFormatting.Items[3].ToString())
                        runParameters.Options.Add(new OptionEntry(OptionName.FormatConvertTable));
                }
            }

            if (cboConvertTables.Text != cboConvertTables.Items[0].ToString())
                runParameters.ByteConvertTable = cboConvertTables.Text;
            else
                runParameters.ByteConvertTable = null;

            if (cboCarouselProfiles.Text != cboCarouselProfiles.Items[0].ToString())
                runParameters.EITCarousel = cboCarouselProfiles.Text;
            else
                runParameters.EITCarousel = null;

            if (dudEPGDays.SelectedIndex == 0)
                runParameters.EPGDays = -1;
            else
                runParameters.EPGDays = Int32.Parse(dudEPGDays.Text);

            if (cboUseDescriptionAs.SelectedIndex == 1)
                runParameters.Options.Add(new OptionEntry(OptionName.UseDescAsCategory));
            else
            {
                if (cboUseDescriptionAs.SelectedIndex == 2)
                    runParameters.Options.Add(new OptionEntry(OptionName.UseDescAsSubtitle));
                else
                {
                    if (cboUseDescriptionAs.SelectedIndex == 3)
                        runParameters.Options.Add(new OptionEntry(OptionName.UseNoDesc));
                }
            } 

            if (cboLocation.SelectedItem != null && ((Country)cboLocation.SelectedItem).Code != string.Empty)
                runParameters.CountryCode = ((Country)cboLocation.SelectedItem).Code;
            else
                runParameters.CountryCode = null;

            if (cboLocationArea.SelectedItem != null)
            {
                int area = ((Area)cboLocationArea.SelectedItem).Code;
                if (area != 0)
                    runParameters.ChannelBouquet = area;
                else
                    runParameters.ChannelBouquet = -1;
            }
            else
                runParameters.ChannelBouquet = -1;

            if (cboLocationRegion.SelectedItem != null)
            {
                int region = ((DomainObjects.Region)cboLocationRegion.SelectedItem).Code;
                if (region != 0)
                    runParameters.ChannelRegion = region;
                else
                    runParameters.ChannelRegion = -1;
            }
            else
                runParameters.ChannelRegion = -1;

            if (cboCharacterSet.SelectedItem != null)
            {
                string characterSet = ((CharacterSet)cboCharacterSet.SelectedItem).Name;
                if (characterSet != string.Empty)
                    runParameters.CharacterSet = characterSet;
                else
                    runParameters.CharacterSet = null;
            }
            else
                runParameters.CharacterSet = null;

            if (cboCharacterSetPriority.Text == cboCharacterSetPriority.Items[1].ToString())
                runParameters.Options.Add(new OptionEntry(OptionName.UseBroadcastCp));

            if (cboInputLanguage.SelectedItem != null)
            {
                if (((LanguageCode)cboInputLanguage.SelectedItem).Code != string.Empty)
                    runParameters.InputLanguage = ((LanguageCode)cboInputLanguage.SelectedItem).Code;
                else
                    runParameters.InputLanguage = null;
            }
            else
                runParameters.InputLanguage = null;

            if (nudSDTPid.Value != 0)
                runParameters.SDTPid = (int)nudSDTPid.Value;
            else
                runParameters.SDTPid = -1;

            if (nudEITPid.Value != 0)
                runParameters.EITPid = (int)nudEITPid.Value;
            else
                runParameters.EITPid = -1;

            if (nudMHW1Pid1.Value != 0)
                runParameters.MHW1Pids = new int[] { (int)nudMHW1Pid1.Value, (int)nudMHW1Pid2.Value };
            else
                runParameters.MHW1Pids = null;

            if (nudMHW2Pid1.Value != 0)
                runParameters.MHW2Pids = new int[] { (int)nudMHW2Pid1.Value, (int)nudMHW2Pid2.Value, (int)nudMHW2Pid3.Value };
            else
                runParameters.MHW2Pids = null;

            if (nudDishNetworkPid.Value != 0)
                runParameters.DishNetworkPid = (int)nudDishNetworkPid.Value;
            else
                runParameters.DishNetworkPid = -1;
            
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cboLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboLocationArea.Items.Clear();

            if (cboLocation.SelectedIndex == -1)
            {
                cboLocationArea.Enabled = false;
                cboLocationRegion.Enabled = false;
                return;
            }

            Country country = cboLocation.SelectedItem as Country;
            if (country == null)
            {
                cboLocationArea.Enabled = false;
                cboLocationRegion.Enabled = false;
                return;
            }

            if (country.Areas == null || country.Areas.Count == 0)
            {
                cboLocationArea.Enabled = false;
                cboLocationRegion.Enabled = false;
                return;
            }

            if (country.Services == null || country.Services.Count == 0)
            {
                cboLocationArea.Enabled = false;
                cboLocationRegion.Enabled = false;
                return;
            }
            
            Service service = null;

            if (frequency.TunerType == TunerType.File || frequency.TunerType == TunerType.Stream)
                service = country.FindService(frequency.CollectionType.ToString());
            else
                service = country.FindService(frequency.CollectionType.ToString(), frequency.TunerType.ToString());
                
            if (service == null || service.Areas == null || service.Areas.Count == 0)
            {
                cboLocationArea.Enabled = false;
                cboLocationRegion.Enabled = false;
                return;
            }

            fillAreaList(service.Areas);
            
            cboLocationArea.Enabled = true;
            cboLocationArea.SelectedItem = null;

            if (runParameters.ChannelBouquet != -1)
            {
                Area area = null;

                foreach (Area listArea in cboLocationArea.Items)
                {
                    if (listArea.Code == runParameters.ChannelBouquet)
                        area = listArea;
                }

                if (area != null)
                {
                    cboLocationArea.SelectedItem = area;
                    cboLocationArea.Text = area.Name;
                    cboLocationArea_SelectedIndexChanged(null, null);
                }
                else
                    cboLocationArea.SelectedIndex = 0;
            }
            else
                cboLocationArea.SelectedIndex = 0;
        }

        private void fillAreaList(Collection<Area> areas)
        {
            foreach (Area area in areas)
                cboLocationArea.Items.Add(area);
        }

        private void cboLocationArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboLocationRegion.Items.Clear();

            Area area = cboLocationArea.SelectedItem as Area;
            if (area == null || area.Regions == null || area.Regions.Count == 0)
            {
                cboLocationRegion.Enabled = false;
                return;
            }

             cboLocationRegion.Enabled = true;

            foreach (Region region in area.Regions)
                cboLocationRegion.Items.Add(region);
            cboLocationRegion.SelectedItem = null;

            if (runParameters.ChannelRegion != -1)
            {
                DomainObjects.Region region = area.FindRegion(runParameters.ChannelRegion);
                if (region != null)
                {
                    cboLocationRegion.SelectedItem = region;
                    cboLocationRegion.Text = region.Name;
                }
                else
                    cboLocationRegion.SelectedIndex = 0;
            }
            else
                cboLocationRegion.SelectedIndex = 0;
        }

        private void cbHexPids_CheckedChanged(object sender, EventArgs e)
        {
            nudSDTPid.Hexadecimal = cbHexPids.Checked;
            nudEITPid.Hexadecimal = cbHexPids.Checked;
            nudMHW1Pid1.Hexadecimal = cbHexPids.Checked;
            nudMHW1Pid2.Hexadecimal = cbHexPids.Checked;
            nudMHW2Pid1.Hexadecimal = cbHexPids.Checked;
            nudMHW2Pid2.Hexadecimal = cbHexPids.Checked;
            nudMHW2Pid3.Hexadecimal = cbHexPids.Checked;
            nudDishNetworkPid.Hexadecimal = cbHexPids.Checked;
        }

        private void cboEITFormatting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEITFormatting.SelectedIndex != 3)
            {
                cboConvertTables.SelectedIndex = 0;
                cboConvertTables.Enabled = false;
            }
            else
                cboConvertTables.Enabled = true;
        }
    }
}

