import 'package:VinMediaCapture/login/Toast.dart';
import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class DocTypeItemAttr {
  int itemID;
  int attrID;
  String attrFieldName;
  int attrDataType;
  int? attrDataLength;
  String attrDescription;
  String attrName;
  String attrImage;
  bool? isMandatory;
  bool? isManualCollect;
  int? displayIDX;
  int? validationRuleID;
  int? disabled;

  DocTypeItemAttr(
    this.itemID,
    this.attrID,
    this.attrFieldName,
    this.attrDataType,
    this.attrDataLength,
    this.attrDescription,
    this.attrName,
    this.attrImage,
    this.isMandatory,
    this.isManualCollect,
    this.displayIDX,
    this.validationRuleID,
    this.disabled,
  );

  factory DocTypeItemAttr.fromJson(Map<String, dynamic> json) {
    return new DocTypeItemAttr(
        json['ItemID'],
        json['AttrID'],
        "", //json['AttrFieldName'] == null ? 0 : json['AttrFieldName'],
        json['AttrDataType'] == null ? 0 : json['AttrDataType'],
        0, //json['AttrDataLength'] == null ? 0 : json['AttrDataLength'],
        "", //json['AttrDescription'] == null ? "" : json['AttrDescription'],
        "", //json['AttrName'],
        "", //json['AttrImage'] == null ? "" : json['AttrImage'],
        json['isMandatory'] == null ? false : json['isMandatory'],
        json['isManualCollect'] == null ? false : json['isManualCollect'],
        json['DisplayIDX'] == null ? 0 : json['DisplayIDX'],
        0, //json['ValidationRuleID'] == null ? "" : json['ValidationRuleID'],
        json['Disabled'] == null ? 0 : json['Disabled']);
  }

  Map<String, dynamic> toJson() {
    return {
      'ItemID': itemID,
      'AttrID': attrID,
      'AttrFieldName': attrFieldName,
      'AttrDataType': attrDataType,
      'AttrDataLength': attrDataLength,
      'AttrDescription': attrDescription,
      'AttrName': attrName,
      'AttrImage': attrImage,
      'isMandatory': isMandatory,
      'isManualCollect': isManualCollect,
      'DisplayIDX': displayIDX,
      'ValidationRuleID': validationRuleID,
      'Disabled': disabled,
    };
  }
}
