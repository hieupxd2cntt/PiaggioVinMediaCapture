import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class DocTypeGuide {
  int id;
  int itemID;
  int attrID;
  int modelID;
  int marketID;
  int colorID;
  int? docTypeID;
  String? guideTXT;
  String? guideImg;
  int? disabled;

  DocTypeGuide(
    this.id,
    this.itemID,
    this.attrID,
    this.modelID,
    this.marketID,
    this.colorID,
    this.docTypeID,
    this.guideTXT,
    this.guideImg,
    this.disabled,
  );

  factory DocTypeGuide.upload(
      int _id,
      int _itemID,
      int _attrID,
      int _modelID,
      int _marketID,
      int _colorID,
      int _docTypeID,
      String _guideTXT,
      String _guideImg,
      int _disabled) {
    return new DocTypeGuide(_id, _itemID, _attrID, _modelID, _marketID,
        _colorID, _docTypeID, _guideTXT, _guideImg, _disabled);
  }
  factory DocTypeGuide.fromJson(Map<String, dynamic> json) {
    return new DocTypeGuide(
        json['id'],
        json['itemID'],
        json['attrID'],
        json['modelID'],
        json['marketID'],
        json['colorID'],
        json['docTypeID'],
        json['guideTXT'],
        json['guideImg'],
        json['disabled']);
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'itemID': itemID,
      'attrID': attrID,
      'modelID': modelID,
      'marketID': marketID,
      'colorID': colorID,
      'docTypeID': docTypeID,
      'guideTXT': guideTXT,
      'guideImg': guideImg,
      'disabled': disabled,
    };
  }
}
