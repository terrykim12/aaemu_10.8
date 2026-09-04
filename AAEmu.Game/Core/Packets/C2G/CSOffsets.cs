namespace AAEmu.Game.Core.Packets.C2G;

public static class CSOffsets
{
    // All opcodes here are updated for version client_12_r208022
    // World
    public const ushort X2EnterWorldPacket = 0x000;
    public const ushort CSAesXorKeyPacket = 0x1AC; // 10.0.2.13 RSA key reply (observed C2S opcode after CTJoin; 1.2 had it elsewhere)
    public const ushort CSLeaveWorldPacket = 0x0B2;
    public const ushort CSCancelLeaveWorldPacket = 0x086;
    public const ushort CSCreateExpeditionPacket = 0xfff;
    public const ushort CSChangeExpeditionSponsorPacket = 0xfff; // TODO : this packet seems like it has been removed.
    public const ushort CSChangeExpeditionRolePolicyPacket = 0xfff;
    public const ushort CSChangeExpeditionMemberRolePacket = 0xfff;
    public const ushort CSChangeExpeditionOwnerPacket = 0xfff;
    public const ushort CSRenameExpeditionPacket = 0xfff;
    public const ushort CSDismissExpeditionPacket = 0xfff;
    public const ushort CSInviteToExpeditionPacket = 0xfff;
    public const ushort CSReplyExpeditionInvitationPacket = 0xfff;
    public const ushort CSLeaveExpeditionPacket = 0xfff;
    public const ushort CSKickFromExpeditionPacket = 0xfff;
    // 0x10 unk packet
    public const ushort CSUpdateDominionTaxRatePacket = 0xfff;
    public const ushort CSFactionImmigrationInvitePacket = 0xfff;
    public const ushort CSFactionImmigrationInviteReplyPacket = 0xfff;
    public const ushort CSFactionImmigrateToOriginPacket = 0xfff;
    public const ushort CSFactionKickToOriginPacket = 0xfff;
    public const ushort CSFactionDeclareHostilePacket = 0xfff;
    public const ushort CSFamilyInviteMemberPacket = 0x0BD;
    public const ushort CSFamilyReplyInvitationPacket = 0x067;
    public const ushort CSFamilyLeavePacket = 0x0CB;
    public const ushort CSFamilyKickPacket = 0x084;
    public const ushort CSFamilyChangeTitlePacket = 0x078;
    public const ushort CSFamilyChangeOwnerPacket = 0x0C1;
    public const ushort CSListCharacterPacket = 0xfff;
    public const ushort CSRefreshInCharacterListPacket = 0x09C;
    public const ushort CSCreateCharacterPacket = 0x13E;
    public const ushort CSEditCharacterPacket = 0x080;
    public const ushort CSDeleteCharacterPacket = 0x0B7;
    public const ushort CSSelectCharacterPacket = 0x09D; // 10.8 (was 0x044 in 10.0)
    public const ushort CSCheckRaceCongestionPacket = 0x04D;
    public const ushort CSSpawnCharacterPacket = 0x114;
    public const ushort CSCancelCharacterDeletePacket = 0x16D;
    public const ushort CSNotifyInGamePacket = 0x192;
    public const ushort CSNotifyInGameCompletedPacket = 0x006;
    public const ushort CSEditorGameModePacket = 0x1BB;
    public const ushort CSChangeTargetPacket = 0x056;
    public const ushort CSRequestCharBriefPacket = 0xfff;
    public const ushort CSSpawnSlavePacket = 0x1F6;
    public const ushort CSDespawnSlavePacket = 0x118;
    public const ushort CSDestroySlavePacket = 0x0A7;
    public const ushort CSBindSlavePacket = 0x017;
    public const ushort CSDiscardSlavePacket = 0xfff;
    public const ushort CSChangeSlaveTargetPacket = 0xfff; // TODO: this packet is not in the offsets
    public const ushort CSChangeSlaveNamePacket = 0x11B;
    public const ushort CSRepairSlaveItemsPacket = 0x213;
    public const ushort CSTurretStatePacket = 0x072;
    public const ushort CSChangeSlaveEquipmentPacket = 0x01D;
    public const ushort CSDestroyItemPacket = 0x05E;
    public const ushort CSSplitBagItemPacket = 0x150;
    public const ushort CSSwapItemsPacket = 0x190;
    public const ushort CSRepairSingleEquipmentPacket = 0x00C;
    public const ushort CSRepairAllEquipmentsPacket = 0x00C;
    public const ushort CSSplitCofferItemPacket = 0x04A;
    public const ushort CSSwapCofferItemsPacket = 0x20D;
    public const ushort CSExpandSlotsPacket = 0x13B;
    public const ushort CSSellBackpackGoodsPacket = 0x159;
    public const ushort CSSpecialtyRatioPacket = 0x166;
    public const ushort CSListSpecialtyGoodsPacket = 0xfff;
    public const ushort CSBuySpecialtyItemPacket = 0xfff; // TODO: this packet is not in the offsets
    public const ushort CSSpecialtyRecordLoadPacket = 0xfff; // TODO: this packet is not in the offsets
    public const ushort CSDepositMoneyPacket = 0xfff;
    public const ushort CSWithdrawMoneyPacket = 0xfff;
    public const ushort CSConvertItemLookPacket = 0x079;
    public const ushort CSItemSecurePacket = 0x199;
    public const ushort CSItemUnsecurePacket = 0x1A9;
    public const ushort CSEquipmentsSecurePacket = 0x02B;
    public const ushort CSEquipmentsUnsecurePacket = 0x185;
    public const ushort CSResurrectCharacterPacket = 0xfff;
    public const ushort CSSetForceAttackPacket = 0xfff;
    public const ushort CSChallengeDuelPacket = 0x19C;
    public const ushort CSStartDuelPacket = 0x0FA;
    public const ushort CSStartSkillPacket = 0x1A5;
    public const ushort CSStopCastingPacket = 0x20B;
    public const ushort CSRemoveBuffPacket = 0x03A;
    public const ushort CSConstructHouseTaxPacket = 0xfff;
    public const ushort CSCreateHousePacket = 0xfff;
    public const ushort CSDecorateHousePacket = 0x207;
    public const ushort CSChangeHouseNamePacket = 0x0AD;
    public const ushort CSChangeHousePermissionPacket = 0x00A;
    public const ushort CSChangeHousePayPacket = 0xfff; // TODO: this packet is not in the offsets
    public const ushort CSRequestHouseTaxPacket = 0xfff;
    // 0x5c unk packet
    public const ushort CSAllowHousingRecoverPacket = 0xfff;
    public const ushort CSSellHousePacket = 0xfff;
    public const ushort CSSellHouseCancelPacket = 0xfff;
    public const ushort CSBuyHousePacket = 0xfff;
    public const ushort CSJoinUserChatChannelPacket = 0x07C;
    public const ushort CSLeaveChatChannelPacket = 0x15C;
    public const ushort CSSendChatMessagePacket = 0xfff;
    public const ushort CSConsoleCmdUsedPacket = 0x18E;
    public const ushort CSInteractNPCPacket = 0x1DE;
    public const ushort CSInteractNPCEndPacket = 0x1D1;
    public const ushort CSBoardingTransferPacket = 0x0B3;
    public const ushort CSStartInteractionPacket = 0x10F;
    public const ushort CSSelectInteractionExPacket = 0xfff;
    public const ushort CSCofferInteractionPacket = 0x0B5;
    public const ushort CSCriminalLockedPacket = 0x1AF;
    public const ushort CSReplyImprisonOrTrialPacket = 0x081;
    public const ushort CSSkipFinalStatementPacket = 0x198;
    public const ushort CSReplyInviteJuryPacket = 0x0C9;
    public const ushort CSJurySummonedPacket = 0x1E7;
    public const ushort CSJuryEndTestimonyPacket = 0x112;
    public const ushort CSCancelTrialPacket = 0x1A6;
    public const ushort CSJuryVerdictPacket = 0xfff;
    public const ushort CSReportCrimePacket = 0x049;
    public const ushort CSJoinTrialAudiencePacket = 0x1B5;
    public const ushort CSLeaveTrialAudiencePacket = 0x14F;
    public const ushort CSRequestJuryWaitingNumberPacket = 0x14A;
    public const ushort CSInviteToTeamPacket = 0x04D;
    public const ushort CSInviteAreaToTeamPacket = 0x17B;
    public const ushort CSReplyToJoinTeamPacket = 0x033;
    public const ushort CSLeaveTeamPacket = 0x0EE;
    public const ushort CSKickTeamMemberPacket = 0x18F;
    public const ushort CSMakeTeamOwnerPacket = 0x08B;
    public const ushort CSSetTeamOfficerPacket = 0xfff; // TODO: this packet is not in the offsets 
    public const ushort CSConvertToRaidTeamPacket = 0xfff;
    public const ushort CSMoveTeamMemberPacket = 0x103;
    public const ushort CSChangeLootingRulePacket = 0x20C;
    public const ushort CSDismissTeamPacket = 0x050;
    public const ushort CSSetTeamMemberRolePacket = 0x20A;
    public const ushort CSSetOverHeadMarkerPacket = 0x011;
    public const ushort CSSetPingPosPacket = 0x01A;
    public const ushort CSAskRiskyTeamActionPacket = 0x050;
    public const ushort CSMoveUnitPacket = 0x104;
    public const ushort CSSkillControllerStatePacket = 0x0FD;
    public const ushort CSCreateSkillControllerPacket = 0x052;
    public const ushort CSActiveWeaponChangedPacket = 0x1B9;
    public const ushort CSChangeItemLookPacket = 0xfff; // TODO: this packet is not in the offsets 
    public const ushort CSLootOpenBagPacket = 0x1EF;
    public const ushort CSLootItemPacket = 0x0D1;
    public const ushort CSLootCloseBagPacket = 0x109;
    public const ushort CSLootDicePacket = 0x1F9;
    public const ushort CSLearnSkillPacket = 0x0F6;
    public const ushort CSLearnBuffPacket = 0x1D8;
    public const ushort CSResetSkillsPacket = 0xfff;
    public const ushort CSSwapAbilityPacket = 0x093;
    public const ushort CSSendMailPacket = 0xfff;
    public const ushort CSListMailPacket = 0x0C3;
    public const ushort CSListMailContinuePacket = 0x038;
    public const ushort CSReadMailPacket = 0x030;
    public const ushort CSTakeAttachmentItemPacket = 0x09A;
    public const ushort CSTakeAttachmentMoneyPacket = 0x1CE;
    // 0x9f unk packet
    public const ushort CSTakeAttachmentSequentially = 0x0E1;
    public const ushort CSPayChargeMoneyPacket = 0xfff;
    public const ushort CSDeleteMailPacket = 0x10D;
    public const ushort CSReportSpamPacket = 0x1BA;
    public const ushort CSReturnMailPacket = 0x076; // TODO: this packet is not in the offsets 
    public const ushort CSRemoveMatePacket = 0x070;
    public const ushort CSChangeMateTargetPacket = 0x180;
    public const ushort CSChangeMateNamePacket = 0x004;
    public const ushort CSMountMatePacket = 0x07F;
    public const ushort CSUnMountMatePacket = 0xfff;
    public const ushort CSChangeMateEquipmentPacket = 0x11A;
    public const ushort CSChangeMateUserStatePacket = 0x0C7;
    // 0xab unk packet
    // 0xac unk packet
    public const ushort CSExpressEmotionPacket = 0x1CF;
    public const ushort CSBuyItemsPacket = 0x148;
    public const ushort CSBuyCoinItemPacket = 0xfff;
    public const ushort CSSellItemsPacket = 0x193;
    public const ushort CSListSoldItemPacket = 0x1C8;
    public const ushort CSBuyPriestBuffPacket = 0xfff;
    public const ushort CSUseTeleportPacket = 0xfff;
    public const ushort CSTeleportEndedPacket = 0xfff;
    public const ushort CSRepairPetItemsPacket = 0x1B8;
    public const ushort CSUpdateActionSlotPacket = 0x00E;
    public const ushort CSAuctionPostPacket = 0xfff;
    public const ushort CSAuctionSearchPacket = 0x025;
    public const ushort CSBidAuctionPacket = 0xfff;
    public const ushort CSCancelAuctionPacket = 0xfff;
    public const ushort CSAuctionMyBidListPacket = 0x0D8;
    public const ushort CSAuctionLowestPricePacket = 0xfff;
    public const ushort CSRollDicePacket = 0xfff;
    //0xbf CSRequestNpcSpawnerList
    //0xc8 CSRemoveAllFieldSlaves
    //0xc9 CSAddFieldSlave
    public const ushort CSHangPacket = 0xfff;
    public const ushort CSUnhangPacket = 0x0ED;
    public const ushort CSUnbondDoodadPacket = 0x10F;
    public const ushort CSCompletedCinemaPacket = 0x0D6;
    public const ushort CSStartedCinemaPacket = 0x124;
    public const ushort CSRequestPermissionToPlayCinemaForDirectingMode = 0xFFF;
    //0xd1 CSEditorRemoveGimmickPacket
    //0xd2 CSEditorAddGimmickPacket
    //0xd3 CSInteractGimmickPacket
    //0xd4 CSWorldRayCastingPacket
    public const ushort CSStartQuestContextPacket = 0x085;
    public const ushort CSCompleteQuestContextPacket = 0x151;
    public const ushort CSDropQuestContextPacket = 0x088;
    public const ushort CSResetQuestContextPacket = 0xfff; // TODO: this packet is not in the offsets 
    public const ushort CSAcceptCheatQuestContextPacket = 0xfff; // TODO: this packet is not in the offsets 
    public const ushort CSQuestTalkMadePacket = 0x07D;
    public const ushort CSQuestStartWithPacket = 0xfff;
    public const ushort CSTryQuestCompleteAsLetItDonePacket = 0x200;
    public const ushort CSUsePortalPacket = 0x010;
    public const ushort CSDeletePortalPacket = 0x014;
    public const ushort CSInstanceLoadedPacket = 0x125;
    public const ushort CSApplyToInstantGamePacket = 0x02A;
    public const ushort CSCancelInstantGamePacket = 0x061;
    public const ushort CSJoinInstantGamePacket = 0x05C;
    public const ushort CSEnteredInstantGameWorldPacket = 0x0A5;
    public const ushort CSLeaveInstantGamePacket = 0xfff;
    // Client re-entry check, sent fire-and-forget in char-list/char-select/in-world. The reference server
    // sends no response; handled as a no-op to stop the "Unknown packet 0x12e" log spam.
    public const ushort CSReentryCheckPacket = 0x12E;
    public const ushort CSCreateDoodadPacket = 0x10B;
    public const ushort CSSaveDoodadUccStringPacket = 0xfff; // TODO: this packet is not in the offsets 
    public const ushort CSNaviTeleportPacket = 0x0BE;
    public const ushort CSNaviOpenPortalPacket = 0x149;
    public const ushort CSChangeDoodadPhasePacket = 0xfff;
    public const ushort CSNaviOpenBountyPacket = 0x097;
    public const ushort CSChangeDoodadDataPacket = 0x1C0;
    public const ushort CSStartTradePacket = 0x0DA;
    public const ushort CSCanStartTradePacket = 0x0CD;
    public const ushort CSCannotStartTradePacket = 0x1F7;
    public const ushort CSCancelTradePacket = 0x1BF;
    public const ushort CSPutupTradeItemPacket = 0x0FE;
    public const ushort CSPutupTradeMoneyPacket = 0x18C;
    public const ushort CSTakedownTradeItemPacket = 0xfff;
    public const ushort CSTradeLockPacket = 0x196;
    public const ushort CSTradeOkPacket = 0xfff;
    public const ushort CSSaveTutorialPacket = 0x0F5;
    public const ushort CSSetLogicDoodadPacket = 0x0D9;
    public const ushort CSCleanupLogicLinkPacket = 0x04B;
    public const ushort CSExecuteCraft = 0x145;
    public const ushort CSChangeAppellationPacket = 0x121;
    public const ushort CSCreateShipyardPacket = 0x0B4;
    public const ushort CSRestartMainQuestPacket = 0x0BC;
    public const ushort CSSetLpManageCharacterPacket = 0x1F4; // 10.0.2.13 CS_SET_LP_MANAGE_CHARACTER (336)
    public const ushort CSUpgradeExpertLimitPacket = 0x179;
    public const ushort CSDowngradeExpertLimitPacket = 0x04E;
    public const ushort CSExpandExpertPacket = 0x1A7;
    public const ushort CSSearchListPacket = 0x201; // 10.8 (was 0xFFF)
    public const ushort CSAddFriendPacket = 0x1E1;
    public const ushort CSDeleteFriendPacket = 0xfff;
    public const ushort CSCharDetailPacket = 0xfff;
    public const ushort CSAddBlockedUserPacket = 0x092;
    public const ushort CSDeleteBlockedUserPacket = 0x139;
    public const ushort CSRequestCommonFarmList = 0xFFF;
    public const ushort CSNotifySubZonePacket = 0x155;
    public const ushort CSResturnAddrsPacket = 0x0DC;
    public const ushort CSRequestUIDataPacket = 0x17C;
    public const ushort CSSaveUIDataPacket = 0x11D;
    public const ushort CSBroadcastVisualOptionPacket = 0x163;
    public const ushort CSRestrictCheckPacket = 0x142;
    public const ushort CSICSMenuListPacket = 0x173;
    public const ushort CSICSGoodsListPacket = 0xFFF;
    public const ushort CSICSBuyGoodPacket = 0xFFF;
    public const ushort CSICSMoneyRequestPacket = 0x152;
    public const ushort CSSendUserMusicPacket = 0x0E4;
    public const ushort CSSaveUserMusicNotesPacket = 0x0A2;
    public const ushort CSRequestMusicNotesPacket = 0x1EA;
    public const ushort CSEndMusicPacket = 0xFFF; // tentative name
    public const ushort CSExitBeautySalonPacket = 0xFFF;
    public const ushort CSBeautyshopDataPacket = 0xfff;
    public const ushort CSEnterBeautySalonPacket = 0xFFF;
    public const ushort CSRankCharacterPacket = 0x117; // 10.8 (was 0xFFF)
    public const ushort CSRequestSecondPasswordKeyTablesPacket = 0xfff;
    // 0x130 CSRankSnapshotPacket
    public const ushort CSRequestSpecialtyCurrentPacket = 0xfff;
    public const ushort CSIdleStatusPacket = 0x0E2;
    // 0x133 CSChangeAutoUseAAPointPacket
    public const ushort CSThisTimeUnpackItemPacket = 0x190;
    public const ushort CSPremiumServiceBuyPacket = 0x002;
    public const ushort CSPremiumServiceListPacket = 0x100; // 10.8 (was 0x08C in 10.0)
    // 0x137 CSICSBuyAAPointPacket
    // 0x138 CSRequestTencentFatigueInfoPacket
    public const ushort CSTakeAllAttachmentItemPacket = 0x18D;
    // 0x13a unk packet
    // 0x13b unk packet
    public const ushort CSPremiumServiceMsgPacket = 0x05F;
    // 0x13d unk packet
    // 0x13e unk packet
    public const ushort CSUnknownInstancePacket = 0xFFF;
    // 0x13f unk packet
    public const ushort CSSetupSecondPassword = 0x1B6; // 10.8 (was 0xFFF)
    // 0x141 unk packet
    // 0x142 unk packet

    // no such packets
    public const ushort CSUpdateNationalTaxRatePacket = 0xfff;
    public const ushort CSSetCraftingPayPacket = 0xfff;
}
