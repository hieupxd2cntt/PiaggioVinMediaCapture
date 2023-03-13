import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class DocTypeItems {
  int? docTypeID;
  int itemID;
  String itemDescription;
  String itemName;
  String itemImage;
  bool isMandatory;
  bool manualCollect;
  int? modelID;
  int? marketID;
  int? colorID;
  int? displayIDX;
  int? disabled;

  DocTypeItems(
    this.docTypeID,
    this.itemID,
    this.itemDescription,
    this.itemName,
    this.itemImage,
    this.isMandatory,
    this.manualCollect,
    this.modelID,
    this.marketID,
    this.colorID,
    this.displayIDX,
    this.disabled,
  );

  factory DocTypeItems.fromJson(Map<String, dynamic> json) {
    return new DocTypeItems(
        json['DocTypeID'],
        json['ItemID'],
        json['ItemDescription'],
        json['ItemName'],
        json['ItemImage'],
        json['IsMandatory'],
        json['ManualCollect'],
        json['ModelID'],
        json['MarketID'],
        json['ColorID'],
        json['DisplayIDX'],
        json['Disabled']);
  }

  Map<String, dynamic> toJson() {
    return {
      'docTypeID': docTypeID,
      'itemID': itemID,
      'itemDescription': itemDescription,
      'itemName': itemName,
      'itemImage': itemImage,
      'isMandatory': isMandatory,
      'manualCollect': manualCollect,
      'modelID': modelID,
      'marketID': marketID,
      'colorID': colorID,
      'displayIDX': displayIDX,
      'disabled': disabled,
    };
  }
}
