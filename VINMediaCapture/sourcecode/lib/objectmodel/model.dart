import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class Model {
  int modelID;
  String? modelCode;
  String? modelName;
  int? disabled;

  Model(
    this.modelID,
    this.modelCode,
    this.modelName,
    this.disabled,
  );

  factory Model.upload(
    int _modelID,
    String? _modelCode,
    String? _modelName,
    int? _disabled,
  ) {
    return new Model(_modelID, _modelCode, _modelName, _disabled);
  }
  factory Model.fromJson(Map<String, dynamic> json) {
    return new Model(
      json['ModelID'],
      json['ModelCode'],
      json['ModelName'],
      json['Disabled'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'ModelID': modelID,
      'ModelCode': modelCode,
      'ModelName': modelName,
      'Disabled': disabled,
    };
  }
}
