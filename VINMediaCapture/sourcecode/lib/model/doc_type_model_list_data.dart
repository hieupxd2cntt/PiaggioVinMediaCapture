import 'dart:convert';
import 'package:VinMediaCapture/apilib/apilib.dart';
import 'package:VinMediaCapture/common/common.dart';
import 'package:VinMediaCapture/objectmodel/DocTypeItemAddModel.dart';
import 'package:flutter_session_manager/flutter_session_manager.dart';

class DocTypeModelListData {
  DocTypeModelListData({
    this.imagePath = '',
    this.assetImage = '',
    this.titleTxt = '',
    this.subTxt = "",
    this.attrDocType = 0,
    this.textValue = "",
    this.itemId = 0,
    this.attrId = 0,
    this.currentSession = "",
    this.attrText = "",
    this.isMandatory = true,
    this.errorValidate = "",
    this.attrDataType = 0,
  });
  DocTypeModelListData.Create(
      this.imagePath,
      this.assetImage,
      this.titleTxt,
      this.subTxt,
      this.attrDocType,
      this.textValue,
      this.itemId,
      this.attrId,
      this.currentSession,
      this.attrText,
      this.isMandatory,
      this.errorValidate,
      this.attrDataType);

  String imagePath;
  String titleTxt;
  String subTxt;
  String assetImage;
  String textValue;
  String currentSession;
  int attrDocType;
  int attrDataType;
  int itemId;
  int attrId;
  String attrText;
  bool isMandatory;
  String errorValidate;
  factory DocTypeModelListData.fromJson(Map<String, dynamic> json) {
    return new DocTypeModelListData.Create(
      json['imagePath'],
      json['assetImage'],
      json['titleTxt'],
      json['subTxt'],
      json['attrDocType'],
      json['textValue'],
      json['itemId'],
      json['attrId'],
      json['currentSession'],
      json['attrText'],
      json['isMandatory'],
      json['errorValidate'],
      json['attrDataType'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'imagePath': imagePath,
      'assetImage': assetImage,
      'titleTxt': titleTxt,
      'subTxt': subTxt,
      'attrDocType': attrDocType,
      'textValue': textValue,
      'itemId': itemId,
      'attrId': attrId,
      'currentSession': currentSession,
      'attrText': attrText,
      'isMandatory': isMandatory,
      'errorValidate': errorValidate,
      'attrDataType': attrDataType
    };
  }

  static Future<List<DocTypeModelListData>> getAttrDataType(
      String _currentSession) async {
    var sessionManager = SessionManager();
    var barCode = await sessionManager.get("currentBarCode");
    _currentSession = await sessionManager.get("currentSession");
    var getListAttribute = await GetListAttribute(barCode);
    var tagObjsJson = jsonDecode(getListAttribute.body) as List;
    List<DocTypeItemAddModel> attrs = tagObjsJson
        .map((tagJson) => DocTypeItemAddModel.fromJson(tagJson))
        .toList();
    ModelList.clear();
    for (var element in attrs) {
      try {
        int attrDocType = 0;
        if (element.docTypeItems != null) {
          attrDocType = element.docTypeItems!.docTypeID!;
        }
        var value = DocTypeModelListData(
            attrDocType: attrDocType,
            imagePath: element.docTypeItems!.itemImage == null
                ? ""
                : hostUrl +
                    element.docTypeItems!
                        .itemImage, //element.docTypeItems?.itemImage,
            subTxt: element.docTypeItems!.itemDescription,
            titleTxt: element.docTypeItems!.itemName,
            itemId: element.docTypeItems!.itemID,
            attrId: element.docTypeItemAttr!.attrID,
            attrText: element.docTypeItemAttr!.attrName,
            currentSession: _currentSession,
            isMandatory: element.docTypeItems!.isMandatory,
            attrDataType: element.docTypeItemAttr!.attrDataType);
        ModelList.add(value);
      } catch (ee) {
        var abc = 1;
      }
    }

    return ModelList;
  }

  static List<DocTypeModelListData> ModelList = <DocTypeModelListData>[
    /* DocTypeModelListData(
      imagePath: 'assets/hotel/hotel_1.png',
      titleTxt: 'Grand Royal Hotel',
      subTxt: 'Wembley, London',
    ),
    DocTypeModelListData(
      imagePath: 'assets/hotel/hotel_2.png',
      titleTxt: 'Queen Hotel',
      subTxt: 'Wembley, London',
    ),*/
    /*DocTypeModelListData(
      imagePath: 'assets/hotel/hotel_3.png',
      titleTxt: 'Grand Royal Hotel',
      subTxt: 'Wembley, London',
    ),
    DocTypeModelListData(
      imagePath: 'assets/hotel/hotel_4.png',
      titleTxt: 'Queen Hotel',
      subTxt: 'Wembley, London',
    ),
    DocTypeModelListData(
      imagePath: 'assets/hotel/hotel_5.png',
      titleTxt: 'Grand Royal Hotel',
      subTxt: 'Wembley, London',
    ),*/
  ];
}
